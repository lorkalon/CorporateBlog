using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Results;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.Models;
using CorporateBlog.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AuthenticationManager = CorporateBlog.WebApi.Authentication.AuthenticationManager;

namespace CorporateBlog.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthenticationManager _manager;

        public AccountController(IUserRegistrationService userRegistrationService)
        {
            _manager = new AuthenticationManager(userRegistrationService);
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpGet]
        public string Register()
        {
            return "Hello!";
        }


        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userModel.RoleId = (int)RoleType.Client;
            IdentityResult result = await _manager.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            var token = _manager.GenerateEmailConfirmationTokenAsync(userModel.Id);
            return Ok();
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

    }
}
