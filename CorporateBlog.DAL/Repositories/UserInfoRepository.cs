using System.Data.Entity;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class UserInfoRepository:GenericRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(IContextCreator contextCreator) : base(contextCreator)
        {
        }
    }
}
