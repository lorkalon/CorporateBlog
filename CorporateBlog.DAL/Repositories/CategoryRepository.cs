using System.Data.Entity;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class CategoryRepository:GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IContextCreator contextCreator) : base(contextCreator)
        {
        }
    }
}
