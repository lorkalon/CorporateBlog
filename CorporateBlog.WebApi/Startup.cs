﻿using System;
using System.Configuration;
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
using Microsoft.Owin;
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

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            DAL.DependeciesResolver.Resolve(builder);
            BLL.DependeciesResolver.Resolve(builder);
            builder.RegisterType<UserStore>().As<IUserStore<ApplicationUser, int>>().InstancePerRequest();
            builder.RegisterType<UserManager<ApplicationUser, int>>().InstancePerRequest();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            UserManager<ApplicationUser, int> userManager;

            using (var scope = config.DependencyResolver.BeginScope())
            {
                userManager = scope.GetService(typeof(UserManager<ApplicationUser, int>)) as UserManager<ApplicationUser, int>;
            }


            ConfigureOAuth(app, userManager);
            ModelsMappers.RegisterMappers();

        }

        //private void CreateDatabase(AuthenticationManager authManager, IUserRegistrationService userRegistrationService)
        //{
        //    //Add roles
        //    RegisterAdmin(authManager, userRegistrationService);
        //    ConfigurationManagerService.DatabaseCreated = true;
        //}

        //private async void RegisterAdmin(AuthenticationManager authManager, IUserRegistrationService userRegistrationService)
        //{
        //    var adminName = ConfigurationManagerService.DefaultAdminName;
        //    var adminPassword = ConfigurationManagerService.DefaultAdminPassword;

        //    if (userRegistrationService.FindUser(adminName) == null)
        //    {
        //        var userModel = new UserModel()
        //        {
        //            Login = adminName,
        //            Password = adminPassword,
        //            RoleId = (int)RoleType.Admin
        //        };

        //        await authManager.RegisterUser(userModel);
        //    }
        //}

        public void ConfigureOAuth(IAppBuilder app, UserManager<ApplicationUser, int> userManager)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CorporateBlogAuthorizationServerProvider(userManager)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}