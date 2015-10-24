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
using CorporateBlog.WebApi.Authentication;
using CorporateBlog.WebApi.Models;
using CorporateBlog.WebApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CorporateBlog.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
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

            var appUser = new ApplicationUser()
            {
                Email = userModel.Email,
                RoleId = (int)RoleType.Client,
                UserName = userModel.Login,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, userModel.Password);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser.Id);
            var callbackUrl = new Uri(Url.Link("EmailConfirmationRoute", new { userId = appUser.Id, code = token }));

            _userManager.EmailService = new EmailService();
            await _userManager.SendEmailAsync(appUser.Id, "Email confirmation",
                "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return Ok();
        }

        [HttpGet]
        [Route("ConfirmEmail", Name = "EmailConfirmationRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(int userId = 0, string code = "")
        {
            if (userId == 0 || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "Invalid confirmation token!");
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
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
