using System.Collections.Generic;
using System.Data.Entity;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;
using CorporateBlog.DAL.Repositories.Filters;

namespace CorporateBlog.DAL.Repositories
{
    public class CategoryRepository:GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IContextCreator contextCreator) : base(contextCreator)
        {
        }
    }
}
