using System;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.Services;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class UserStore : BaseService, IUserPasswordStore<ApplicationUser, int>, IUserEmailStore<ApplicationUser, int>
    {
        private readonly IUserRepository _userRepository;

        public UserStore(IUserRepository userRepository, IContextProvider contextProvider) : base(contextProvider)
        {
            _userRepository = userRepository;
        }


        public void Dispose()
        {

        }

        public async Task CreateAsync(ApplicationUser user)
        {

            var mappedUser = Mapper.Map<DAL.Models.User>(user);
            _userRepository.Add(mappedUser);
            await SaveChangesAsync();
            user.Id = mappedUser.Id;
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            var savedUser = await _userRepository.FindUserAsync(user.Id);
            savedUser.PasswordHash = user.PasswordHash;
            _userRepository.Update(savedUser);
            await SaveChangesAsync();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> FindByIdAsync(int userId)
        {
            var user = await _userRepository.FindUserAsync(userId);
            var applicationUser = Mapper.Map<ApplicationUser>(user);
            return applicationUser;
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = await _userRepository.FindUserAsync(userName);
            var applicationUser = Mapper.Map<ApplicationUser>(user);
            return applicationUser;
        }

        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            await Task.Run(()=>user.PasswordHash = passwordHash);
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

        public async Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            var savedUser = await _userRepository.FindUserByEmailAsync(user.Email);
            savedUser.EmailConfirmed = confirmed;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userRepository.FindUserByEmailAsync(email);
            var applicationUser = Mapper.Map<ApplicationUser>(user);
            return applicationUser;
        }
    }
}