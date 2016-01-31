using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.BLL.IServices
{
    public interface IUserService
    {
        Common.UsersReport GetUsersReport(Common.Filters.UsersFilter filter);
        Task UpdateUser(Common.GeneralUserInfo info);
        Task DeleteUser(int userId);
    }
}
