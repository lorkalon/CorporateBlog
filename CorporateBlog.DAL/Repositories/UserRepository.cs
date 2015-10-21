using System.Data.Entity;
using System.Linq;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {
        private readonly IContextCreator _contextCreator;
        public UserRepository(IContextCreator contextCreator) : base(contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public User FindUser(string login)
        {
            return _contextCreator.GetContext.Set<User>().FirstOrDefault(user => user.Login == login);
        }

        public User FindUser(int userId)
        {
            return _contextCreator.GetContext.Set<User>().FirstOrDefault(user => user.Id == userId);
        }
    }
}
