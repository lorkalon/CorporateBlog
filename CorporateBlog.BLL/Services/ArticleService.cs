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
using CorporateBlog.DAL.Models.Filters;

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

        public IEnumerable<Common.Article> GetPagedByFilter(Common.Filters.ArticleFilter filter)
        {
            var whereExpressions = new List<Expression<Func<DAL.Models.Article, bool>>>()
            {
               a=>!(filter.CategoryId.HasValue) || (filter.CategoryId.HasValue && a.CategoryId == filter.CategoryId),
               a=>!(filter.CreatedById.HasValue) || (filter.CreatedById.HasValue && a.UserId == filter.CreatedById),
               a=> filter.SearchContent == null || (a.Text.ToLower().Contains(filter.SearchContent.ToLower()) || 
                                                    a.Title.ToLower().Contains(filter.SearchContent.ToLower())),
            };

            var articles = _articleRepository.GetPaged(whereExpressions, null, Mapper.Map<DAL.Models.Filters.BaseFilter>(filter));

            return articles.Select(Mapper.Map<Common.Article>);
        }
    }
}
