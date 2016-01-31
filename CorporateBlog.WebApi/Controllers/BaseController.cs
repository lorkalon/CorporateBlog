using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using CorporateBlog.WebApi.Authentication;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Controllers
{
    public abstract class BaseController:ApiController
    {
        private readonly ApplicationUserManager _userManager;

        protected BaseController(ApplicationUserManager userManager = null)
        {
            _userManager = userManager;
        }

        protected async Task<ApplicationUser> GetCurrentUser()
        {
            if (_userManager == null)
            {
                throw new Exception("ApplicationUserManager is not instanced!");
            }

            var userName = User.Identity.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        protected class ForbiddenResult : IHttpActionResult
        {
            private readonly HttpRequestMessage _request;
            private readonly string _reason;

            public ForbiddenResult(HttpRequestMessage request, string reason)
            {
                _request = request;
                _reason = reason;
            }

            public ForbiddenResult(HttpRequestMessage request)
            {
                _request = request;
                _reason = "Forbidden";
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = _request.CreateResponse(HttpStatusCode.Forbidden, _reason);
                return Task.FromResult(response);
            }
        }

        protected class ConflictResult : IHttpActionResult
        {
            private readonly HttpRequestMessage _request;
            private readonly string _reason;

            public ConflictResult(HttpRequestMessage request, string reason)
            {
                _request = request;
                _reason = reason;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = _request.CreateResponse(HttpStatusCode.Conflict, _reason);
                return Task.FromResult(response);
            }

        }


        protected virtual ForbiddenResult Forbidden(string message)
        {
            return new ForbiddenResult(Request, message);
        }

        protected virtual ConflictResult Conflict(string message)
        {
            return new ConflictResult(Request, message);
        }
    }
}