using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CorporateBlog.DAL;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.Models;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class UserStore : IUserPasswordStore<ApplicationUser, int>
    {
        private readonly CorporateBlogContext _dbContext = new CorporateBlogContext();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            _dbContext.Users.Add(new User()
                {
                    Login = user.UserName,
                    Password = user.PasswordHash
                });

            return Task.Run(() => _dbContext.SaveChanges());
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task.FromResult<ApplicationUser>(null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            return Task.Run(() => user.PasswordHash);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}