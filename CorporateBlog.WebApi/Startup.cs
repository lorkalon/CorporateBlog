using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.UI.WebControls;
using Autofac;
using Autofac.Integration.WebApi;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.Models;
using CorporateBlog.WebApi.App_Start;
using CorporateBlog.WebApi.Authentication;
using CorporateBlog.WebApi.Mappers;
using CorporateBlog.WebApi.Models;
using CorporateBlog.WebApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;


[assembly: OwinStartup(typeof(CorporateBlog.WebApi.Startup))]
namespace CorporateBlog.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var builder = new ContainerBuilder();

            InitializeBuilder(builder);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            ModelsMappers.RegisterMappers();
            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            CreateDatabase(config.DependencyResolver);
        }

        private void InitializeBuilder(ContainerBuilder builder)
        {
            DAL.DependeciesResolver.Resolve(builder);
            BLL.DependeciesResolver.Resolve(builder);
            builder.RegisterType<UserStore>().As<IUserStore<ApplicationUser, int>>();
            builder.RegisterType<ApplicationUserManager>().InstancePerRequest();
            builder.RegisterType<DpapiDataProtectionProvider>().SingleInstance();
            builder.RegisterType<EmailService>().As<IIdentityMessageService>().InstancePerRequest();
            builder.RegisterType<CorporateBlogAuthorizationServerProvider>().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        private void CreateDatabase(IDependencyResolver resolver)
        {
            using (var scope = resolver.BeginScope())
            {
                RegisterRoles(scope);
                RegisterAdmin(scope);
            }
        }

        private void RegisterRoles(IDependencyScope scope)
        {
            var roleService =
                   scope.GetService(typeof(IRoleService)) as IRoleService;

            if (roleService == null)
            {
                throw new NullReferenceException("RoleService has not been initialized.");
            }

            if (!roleService.GetRoles().Any())
            {
                roleService.AddRoles(new List<Common.Role>()
                {
                    new Common.Role()
                    {
                        Name = "Admin",
                        Id = (int) RoleType.Admin
                    },
                    new Common.Role()
                    {
                        Name = "Publisher",
                        Id = (int) RoleType.Publisher
                    },
                    new Common.Role()
                    {
                        Name = "Client",
                        Id = (int) RoleType.Client
                    }
                });
            }
        }

        private async void RegisterAdmin(IDependencyScope scope)
        {
            var adminName = ConfigurationManagerService.DefaultAdminName;
            var adminPassword = ConfigurationManagerService.DefaultAdminPassword;

            var userManager =
                    scope.GetService(typeof(ApplicationUserManager)) as ApplicationUserManager;

            if (userManager == null)
            {
                throw new NullReferenceException("ApplicationUserManager has not been initialized.");
            }

            var admin = await userManager.FindAsync(adminName, adminPassword);

            if (admin == null)
            {
                await userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = adminName,
                    Email = null,
                    RoleId = (int) RoleType.Admin,
                    EmailConfirmed = true,
                    Blocked = false
                }, adminPassword);
            }
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CorporateBlogAuthorizationServerProvider(),
                AccessTokenProvider = new AuthenticationTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}