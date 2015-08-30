using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class AuthenticationManager : IDisposable
    {
        private UserManager<ApplicationUser, int> _userManager;

        public AuthenticationManager(IUserRegistrationService userRegistrationService)
        {
            _userManager = new UserManager<ApplicationUser, int>(new UserStore(userRegistrationService));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var result = await _userManager.CreateAsync(Mapper.Map<ApplicationUser>(userModel), userModel.Password);
            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _userManager.Dispose();

        }
    }
}
