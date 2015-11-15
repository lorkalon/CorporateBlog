using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.BLL.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IContextProvider contextProvider, IRoleRepository roleRepository) : base(contextProvider)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await _roleRepository.GetAllAsync();
            var mappedRoles = roles.Select(Mapper.Map<Common.Role>);
            return mappedRoles;
        }

        public async Task CreateRolesAsync(IEnumerable<Common.Role> roles)
        {
            foreach (var role in roles)
            {
                _roleRepository.Add(Mapper.Map<DAL.Models.Role>(role));
            }

            await SaveChangesAsync();
        }
    }
}
