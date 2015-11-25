using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.Common.Filters;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.BLL.Services
{
    public class ArticleService:BaseService, IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository, 
                              IContextProvider contextProvider): base(contextProvider)
        {
            _articleRepository = articleRepository;
        }

        public void CreateArticle()
        {
            
        }

        public IEnumerable<Common.Article> GetByDateRange(Common.Filters.ArticlesDateRangeFilter filter)
        {
            Expression<Func<DAL.Models.Article, bool>> whereExpressions = a => a.CategoryId == filter.CategoryId &&
                                                                               a.CreatedOnUtc > filter.StartDate &&
                                                                               a.CreatedOnUtc < filter.EndDate;
            

            Expression<Func<DAL.Models.Article, object>> orderBy = article => article.CreatedOnUtc;

            var articles = _articleRepository.GetFiltered(whereExpressions, orderBy, null, null, false);

            return articles.Select(Mapper.Map<Common.Article>);
        } 
    }
}
