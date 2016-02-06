
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindUserAsync(string login);
        Task<User> FindUserAsync(int userId);
        Task<User> FindUserByEmailAsync(string email);
        User FindUser(string login);
        User FindUser(int userId);
    }
}
