using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.BLL.IServices
{
    public interface IUserRegistrationService
    {
        IEnumerable<Common.Role> GetRoles();

        void AddUser(Common.User user);

        Common.User FindUser(string login);
    }
}
