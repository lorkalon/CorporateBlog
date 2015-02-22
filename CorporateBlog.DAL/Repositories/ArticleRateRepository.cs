using System.Data.Entity;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class ArticleRateRepository: GenericRepository<Common.ArticleRate, ArticleRate>, IArticleRateRepository
    {
        public ArticleRateRepository(DbContext context) : base(context)
        {
        }
    }
}
