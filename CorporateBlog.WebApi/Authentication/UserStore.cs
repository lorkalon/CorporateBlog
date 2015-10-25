using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.BLL.Services;
using CorporateBlog.DAL;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.Models;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class UserStore : BaseService, IUserPasswordStore<ApplicationUser, int>, IUserEmailStore<ApplicationUser, int>
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IContextProvider _contextProvider;

        public UserStore(IUserRegistrationService userRegistrationService, IContextProvider contextProvider) : base(contextProvider)
        {
            _userRegistrationService = userRegistrationService;
            _contextProvider = contextProvider;
        }


        public void Dispose()
        {
            
        }

        public Task CreateAsync(ApplicationUser user)
        {
            var mappedUser = Mapper.Map<Common.User>(user);
             _userRegistrationService.AddUser(mappedUser);
         
            return Task.Run(() => user.Id = mappedUser.Id);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            Common.User user = _userRegistrationService.FindUser(userId);
            var applicationUser = Mapper.Map<ApplicationUser>(user);
            return Task.Run(() => applicationUser);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            Common.User user = _userRegistrationService.FindUser(userName);
            var applicationUser = Mapper.Map<ApplicationUser>(user);
            return Task.Run(() => applicationUser);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.Run(() => user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            return Task.Run(() => user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            var mappedUser = Mapper.Map<Common.User>(user);
            mappedUser.EmailConfirmed = confirmed;
            return Task.Run(() => _userRegistrationService.ConfirmUser(mappedUser));
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            Common.User user = _userRegistrationService.FindUserByEmail(email);
            var applicationUser = Mapper.Map<ApplicationUser>(user);
            return Task.Run(() => applicationUser);
        }
    }
}