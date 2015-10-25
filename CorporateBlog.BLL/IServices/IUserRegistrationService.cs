using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.BLL.IServices
{
    public interface IUserRegistrationService
    {
       
        void AddUser(Common.User user);
        void ConfirmUser(Common.User user);
        void SetUserToRole(Common.User user, RoleType role);

        Common.User FindUser(string login);
        Common.User FindUser(int userId);
        Common.User FindUserByEmail(string email);


    }
}
