using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateBlog.Common.Filters;

namespace CorporateBlog.BLL.IServices
{
    public interface IArticleService
    {
        Task CreateArticleAsync(Common.Article article);
        IEnumerable<Common.Article> GetByDateRange(Common.Filters.ArticlesDateRangeFilter filter);
        Task UpdateArticle(Common.Article article);
        Task DeleteArticle(int articleId);
        Task<Common.Article> GetArticle(int articleId);
        IEnumerable<DateTime> GetDateLimit(int categoryId);
    }
}
