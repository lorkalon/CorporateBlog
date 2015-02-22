using System.Data.Entity;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class UserInfoRepository:GenericRepository<Common.UserInfo, UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(DbContext context) : base(context)
        {
        }
    }
}
