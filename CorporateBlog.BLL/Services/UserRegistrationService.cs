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

        public IEnumerable<Common.Role> GetRoles()
        {
            var roles = _roleRepository.GetPaged();
            var mappedRoles = roles.Select(Mapper.Map<Common.Role>);
            return mappedRoles;
        }

        public void AddUser(Common.User user)
        {
            var mappedUser = Mapper.Map<DAL.Models.User>(user);

            mappedUser.EmailConfirmed = false;
            mappedUser.Blocked = false;

            _userRepository.Add(mappedUser);
            SaveChanges();
            user.Id = mappedUser.Id;
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
    }
}
