using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.WebApi.App_Start;
using CorporateBlog.WebApi.Authentication;
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

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            var dalResolver = new DAL.DependeciesResolver(builder);
            var bllResolver = new BLL.DependeciesResolver(builder);

            dalResolver.RegisterTypes();
            bllResolver.RegisterTypes();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CorporateBlogAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}