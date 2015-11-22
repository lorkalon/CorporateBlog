using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using CorporateBlog.BLL.IServices;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace CorporateBlog.WebApi.Authentication
{
    public class CorporateBlogAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var scope = context.OwinContext.GetAutofacLifetimeScope())
            {
                using (var userManager = scope.Resolve<ApplicationUserManager>())
                {
                    ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

                    if (user == null)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }

                    if (!user.EmailConfirmed)
                    {
                        context.SetError("invalid_grant", "Account is not confirmed!");
                        return;
                    }


                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

                    var roleName = user.RoleName.ToString();
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleName));

                    var properties = CreateProperties(context.UserName, roleName);
                    var ticket = new AuthenticationTicket(identity, properties);

                    context.Validated(ticket);
                }
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, string roleName)
        {
            var data = new Dictionary<string, string>
                        {
                            { "userName", userName },
                            { "role", roleName }
                        };

            return new AuthenticationProperties(data);
        }
    }
}