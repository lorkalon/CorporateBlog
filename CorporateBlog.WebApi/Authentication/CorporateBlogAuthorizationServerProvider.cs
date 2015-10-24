using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using CorporateBlog.BLL.IServices;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Autofac.Integration.Owin;

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

                    if (!user.Confirmed)
                    {
                        context.SetError("invalid_grant", "Account is not confirmed!");
                        return;
                    }


                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim("role", "user"));

                    context.Validated(identity);
                }
            }
        }
    }
}