using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
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
        private readonly IUserInfoService _userInfoService;

        public AccountController(ApplicationUserManager userManager, IUserInfoService userInfoService)
            : base(userManager)
        {
            _userManager = userManager;
            _userInfoService = userInfoService;
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

            var userNameSaved = await _userManager.FindByNameAsync(userModel.UserName);

            if (userNameSaved != null)
            {
                return Conflict("User with the same UserName has already existed!");
            }

            var userEmailSaved = await _userManager.FindByEmailAsync(userModel.Email);

            if (userEmailSaved != null)
            {
                return Conflict("User with the same Email has already existed!");
            }

            userModel.RoleId = (int)RoleType.Client;
            var appUser = Mapper.Map<ApplicationUser>(userModel);

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
        [AllowAnonymous]
        [Route("ConfirmEmail", Name = "EmailConfirmationRoute")]
        public async Task<HttpResponseMessage> ConfirmEmail(int userId = 0, string code = "")
        {
            HttpResponseMessage response;
            string errorUri = "/#/error";
            string root = "http://" + Request.RequestUri.Authority;
            string message = "";

            if (userId == 0 || string.IsNullOrWhiteSpace(code))
            {
                message = "Invalid confirmation link!";
                response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri(root + errorUri + "?message=" + message);
                return response;
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri(root);
                return response;
            }
            else
            {
                message = "Invalid confirmation link!";
                response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri(root + errorUri + "?message=" + message);
                return response;
            }

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("SendResetPasswordToken")]
        public async Task<IHttpActionResult> SendResetPasswordToken([FromUri]string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Forbidden("Email has not been found.");
            }

            var baseUri = "http://" + Request.RequestUri.Authority;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            var encodedToken = HttpUtility.UrlEncode(token);

            var callbackUrl = baseUri + "/#/resetPassword?code=" + encodedToken + "&" + "email=" + email;

            await _userManager.SendEmailAsync(user.Id, "Reset password",
              "Reset password link <a href=\"" + callbackUrl + "\">here</a>");
            return Ok();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword", Name = "ResetPasswordRoute")]
        public async Task<IHttpActionResult> ResetPassword([FromBody]ResetPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Forbidden(String.Format("User with email '{0}' hasn't been registered.", model.Email));
            }

            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Forbidden("Reset Password Code is not valid.");
        }

        [HttpPost]
        [ExtendedAuthorize]
        [Route("SaveUserPicture")]
        public async Task<IHttpActionResult> PostPicture()
        {
            var localPath = ConfigurationManagerService.AvatarsStoragePath;
            var avatarName = await SaveAvatar(localPath);
            if (avatarName == null)
            {
                throw new ArgumentNullException("Posted picture is null!");
            }

            var currentUser = await GetCurrentUser();
            var userInfo = _userInfoService.FindUserInfoByUserId(currentUser.Id);

            if (userInfo != null)
            {
                if (userInfo.Avatar != null)
                {
                    File.Delete(localPath + userInfo.Avatar);
                }
            }

            await _userInfoService.AddOrUpdateUserInfo(new Common.UserInfo()
            {
                UserId = currentUser.Id,
                Avatar = avatarName
            });

            return Ok();
        }

        [ExtendedAuthorize]
        private async Task<string> SaveAvatar(string localPath)
        {
            HttpRequestMessage request = this.Request;
            var randomName = Guid.NewGuid() + ".jpg";
            var provider = await request.Content.ReadAsMultipartAsync();
            var content = provider.Contents.FirstOrDefault();

            if (content == null)
            {
                return null;
            }

            var stream = await content.ReadAsStreamAsync();
            using (var output = new FileStream(localPath + randomName, FileMode.CreateNew))
            {
                await stream.CopyToAsync(output);
            }

            return randomName;
        }

        [HttpDelete]
        [ExtendedAuthorize]
        [Route("DeleteUserPicture")]
        public async Task DeleteAvatar()
        {
            var currentUser = await GetCurrentUser();

            if (currentUser.UserInfo != null)
            {
                var userInfo = currentUser.UserInfo;

                if (userInfo.Avatar != null)
                {
                    File.Delete(ConfigurationManagerService.AvatarsStoragePath + currentUser.UserInfo.Avatar);
                    userInfo.Avatar = null;
                    await _userInfoService.AddOrUpdateUserInfo(userInfo);
                }
            }
        }

        [HttpGet]
        [ExtendedAuthorize]
        [Route("GetMyProfileInfo")]
        public async Task<UserModel> GetMyProfileInfo()
        {
            var currentUser = await GetCurrentUser();
            return Mapper.Map<WebApi.Models.UserModel>(currentUser);
        }

        [HttpPost]
        [ExtendedAuthorize]
        [Route("ChangePassword")]
        public async Task ChangePassword(Models.ChangePassword model)
        {
            var currentUser = await GetCurrentUser();
            await _userManager.ChangePasswordAsync(currentUser.Id, model.OldPassword, model.NewPassword);
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
                        ModelState.AddModelError("error", error);
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
