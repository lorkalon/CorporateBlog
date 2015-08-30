using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.Models;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class UserStore : IUserPasswordStore<ApplicationUser, int>
    {
        private readonly IUserRegistrationService _userRegistrationService;
        public UserStore(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            return Task.Run(() => _userRegistrationService.AddUser(Mapper.Map<Common.User>(user)));
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
            return Task.Run(() => user.PasswordHash = passwordHash);
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