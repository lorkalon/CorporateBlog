using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.Common;

namespace CorporateBlog.BLL.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        Task CreateRolesAsync(IEnumerable<Common.Role> roles);
    }
}
