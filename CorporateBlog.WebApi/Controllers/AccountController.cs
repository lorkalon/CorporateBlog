﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.ModelBinding;
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
    public class AccountController : BaseController
    {
        private readonly ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
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
                EmailConfirmed = false,
                Blocked = false
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, userModel.Password);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser.Id);
            var callbackUrl = new Uri(Url.Link("EmailConfirmationRoute", new { userId = appUser.Id, code = token }));

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

        [HttpGet]
        [Route("SendResetPasswordToken")]
        public async Task<IHttpActionResult> SendResetPasswordToken([FromUri]string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Forbidden("Email has not been found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = new Uri(Url.Link("ResetPasswordRoute", new { userId = user.Id, code = token }));
            await _userManager.SendEmailAsync(user.Id, "Reset password",
              "Reset password link <a href=\"" + callbackUrl + "\">here</a>");
            return Ok();
        }

        [HttpGet]
        [Route("ResetPassword", Name = "ResetPasswordRoute")]
        public async Task<IHttpActionResult> ResetPassword(int userId = 0, string code = "")
        {
            var result = _userManager.ResetPasswordAsync(userId, code, "7654321");
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
