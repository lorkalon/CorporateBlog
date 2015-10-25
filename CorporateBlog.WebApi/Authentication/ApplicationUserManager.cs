using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateBlog.WebApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace CorporateBlog.WebApi.Authentication
{
    public class ApplicationUserManager:UserManager<ApplicationUser, int>, IDisposable
    {
        public ApplicationUserManager(
            DpapiDataProtectionProvider protectionProvider,
            IUserStore<ApplicationUser, int> store,
            IIdentityMessageService emailService ) : base(store)
        {
            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(protectionProvider.Create("EmailConfirmation"));
            EmailService = emailService;
        }

        public void Dispose()
        {
            
        }
    }
}