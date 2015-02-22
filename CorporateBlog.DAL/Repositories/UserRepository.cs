using System.Data.Entity;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class UserRepository:GenericRepository<Common.User, User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
