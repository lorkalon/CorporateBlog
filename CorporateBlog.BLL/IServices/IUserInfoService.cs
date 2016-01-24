using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.BLL.IServices
{
    public interface IUserInfoService
    {
        Task AddOrUpdateUserInfo(Common.UserInfo userInfo);
        Common.UserInfo FindUserInfoByUserId(int userId);
    }
}
