using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<User> FindUserAsync(string login)
        {
            return await _contextCreator.GetContext.Set<User>().FirstOrDefaultAsync(user => user.UserName == login);
        }

        public async Task<User> FindUserAsync(int userId)
        {
            return await _contextCreator.GetContext.Set<User>().FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _contextCreator.GetContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email);
        }

        public User FindUser(int userId)
        {
            return _contextCreator.GetContext.Set<User>().FirstOrDefault(user => user.Id == userId);
        }

        public User FindUser(string login)
        {
            return _contextCreator.GetContext.Set<User>().FirstOrDefault(user => user.UserName == login);
        }
    }
}
