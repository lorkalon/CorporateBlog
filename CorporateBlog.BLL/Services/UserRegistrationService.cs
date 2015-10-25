using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;
using User = CorporateBlog.Common.User;

namespace CorporateBlog.BLL.Services
{
    public class UserRegistrationService : BaseService, IUserRegistrationService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IContextProvider contextProvider,
                                       IRoleRepository roleRepository,
                                       IUserRepository userRepository) : base(contextProvider)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public void AddUser(Common.User user)
        {
            var mappedUser = Mapper.Map<DAL.Models.User>(user);
            _userRepository.Add(mappedUser);
            SaveChanges();
            user.Id = mappedUser.Id;
        }

        public void ConfirmUser(Common.User user)
        {
            var saved =_userRepository.FindUser(user.Id);
            if (saved != null)
            {
                saved.EmailConfirmed = true;
            }

            SaveChanges();
        }

        public void SetUserToRole(Common.User user, RoleType role)
        {
            var saved = _userRepository.FindUser(user.Id);
            if (saved != null)
            {
                saved.RoleId = (int)role;
            }

            SaveChanges();
        }

        public Common.User FindUser(string login)
        {
            var dalUSer = _userRepository.FindUser(login);
            if (dalUSer == null)
            {
                return null;
            }

            return Mapper.Map<Common.User>(dalUSer);
        }

        public Common.User FindUser(int userId)
        {
            var dalUSer = _userRepository.FindUser(userId);
            if (dalUSer == null)
            {
                return null;
            }

            return Mapper.Map<Common.User>(dalUSer);
            
        }

        public User FindUserByEmail(string email)
        {
            var dalUSer = _userRepository.FindUserByEmail(email);
            if (dalUSer == null)
            {
                return null;
            }

            return Mapper.Map<Common.User>(dalUSer);
        }
    }
}
