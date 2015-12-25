using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task CreateArticleAsync(Common.Article article)
        {
            var model = Mapper.Map<DAL.Models.Article>(article);
            _articleRepository.Add(model);
            await SaveChangesAsync();
            article.Id = model.Id;
        }

        public async Task DeleteArticle(int articleId)
        {
            var article = await _articleRepository.GetAsync(articleId);

            if (article != null)
            {
                _articleRepository.Delete(article);
                await SaveChangesAsync();
            }
        }

        public async Task UpdateArticle(Common.Article article)
        {
            var entity = await _articleRepository.GetAsync(article.Id);

            if (entity != null)
            {
                entity.Title = article.Title;
                entity.Text = article.Text;
                entity.CategoryId = article.CategoryId;

                await SaveChangesAsync();
            }
        }

        public IEnumerable<Common.Article> GetByDateRange(Common.Filters.ArticlesDateRangeFilter filter)
        {
            Expression<Func<DAL.Models.Article, bool>> whereExpressions = a => a.CategoryId == filter.CategoryId &&
                                                                               a.CreatedOnUtc >= filter.StartDate &&
                                                                               a.CreatedOnUtc <= filter.EndDate;
            

            Expression<Func<DAL.Models.Article, object>> orderBy = article => article.CreatedOnUtc;

            var articles =
                _articleRepository.GetFiltered(whereExpressions, orderBy, null, null, false).AsQueryable()
                    .Project().To<Common.Article>().ToList();

            return articles;
        }

        public async Task<Common.Article> GetArticle(int articleId)
        {
            var article = await _articleRepository.GetAsync(articleId);
            return Mapper.Map<Common.Article>(article);
        }
    }
}
