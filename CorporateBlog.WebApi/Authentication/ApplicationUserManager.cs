using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace CorporateBlog.WebApi.Authentication
{
    public class ApplicationUserManager:UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(DpapiDataProtectionProvider protectionProvider, IUserStore<ApplicationUser, int> store) : base(store)
        {
            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(protectionProvider.Create("EmailConfirmation"));
        }
    }
}