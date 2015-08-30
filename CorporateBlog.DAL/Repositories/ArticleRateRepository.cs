using System.Data.Entity;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class ArticleRateRepository: GenericRepository<ArticleRate>, IArticleRateRepository
    {
        public ArticleRateRepository(IContextCreator contextCreator) : base(contextCreator)
        {
        }
    }
}
