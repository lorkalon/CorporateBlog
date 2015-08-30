using System;
using System.Data.Entity;
using System.Threading.Tasks;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class AuthenticationManager : IDisposable
    {
        private DbContext _ctx;

        private UserManager<ApplicationUser, int> _userManager;

        public AuthenticationManager()
        {
            _ctx = new CorporateBlogContext();
            _userManager = new UserManager<ApplicationUser, int>(new UserStore());
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new ApplicationUser
            {
                UserName = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
