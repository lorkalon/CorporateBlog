using System;
using System.Data.Entity;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Web.Routing;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.WebApi.Models;
using CorporateBlog.WebApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CorporateBlog.WebApi.Authentication
{
    public class AuthenticationManager : IDisposable
    {
        private UserManager<ApplicationUser, int> _userManager;

        public AuthenticationManager(IUserRegistrationService userRegistrationService)
        {
            _userManager = new UserManager<ApplicationUser, int>(new UserStore(userRegistrationService));
            _userManager.EmailService = new EmailService();
        }

        
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var applicationUser = Mapper.Map<ApplicationUser>(userModel);
            var result = await _userManager.CreateAsync(applicationUser, userModel.Password);
            userModel.Id = applicationUser.Id;
            return result;
        }

        public async Task<bool> LoginUser(UserModel userModel)
        {
            var result = await _userManager.CheckPasswordAsync(Mapper.Map<ApplicationUser>(userModel), userModel.Password);
            return result;
        } 

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(int userId)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public void Dispose()
        {
            _userManager.Dispose();

        }
    }
}
