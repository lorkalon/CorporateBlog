using System.Security.Claims;
using System.Threading.Tasks;
using CorporateBlog.BLL.IServices;
using Microsoft.Owin.Security.OAuth;

namespace CorporateBlog.WebApi.Authentication
{
    public class CorporateBlogAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public CorporateBlogAuthorizationServerProvider(IUserRegistrationService userRegistrationService):base()
        {
            _userRegistrationService = userRegistrationService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var authenticationManager = new AuthenticationManager(_userRegistrationService))
            {
                ApplicationUser user = await authenticationManager.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}