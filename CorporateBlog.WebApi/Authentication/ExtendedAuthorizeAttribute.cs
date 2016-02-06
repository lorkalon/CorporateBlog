using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Autofac;
using Autofac.Integration.Owin;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.WebApi.Authentication
{
    public class ExtendedAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAuthorized = base.IsAuthorized(actionContext);

            if (!isAuthorized)
            {
                return false;
            }

            var identity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            var role = identity?.FindFirst(ClaimTypes.Role);
            if (role == null)
            {
                return false;
            }

            var scope = actionContext.Request.GetOwinContext().GetAutofacLifetimeScope();
            var userRepository = scope.Resolve<IUserRepository>();
            var userName = identity.Name;
            var savedUser = userRepository.FindUser(userName);

            if (savedUser == null)
            {
                return false;
            }

            return savedUser.Role.Name == role.Value;
        }

    }
}