using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common.Filters;
using CorporateBlog.DAL.Models;
using CorporateBlog.WebApi.Authentication;
using CorporateBlog.WebApi.Models;
using Microsoft.AspNet.Identity;
using ArticlesDateRangeFilter = CorporateBlog.WebApi.Models.Filters.ArticlesDateRangeFilter;

namespace CorporateBlog.WebApi.Controllers
{
    [Authorize]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly IArticleRateService _articleRateService;

        public ArticleController(
            IArticleService articleService, 
            IArticleRateService articleRateService, 
            ApplicationUserManager userManager):base(userManager)
        {
            _articleService = articleService;
            _articleRateService = articleRateService;
        }

        [HttpGet]
        [Route("api/Article/GetByDateRange")]
        public async Task<IEnumerable<Models.Article>> GetArticles([FromUri]ArticlesDateRangeFilter filter)
        {
            var currentUser = await GetCurrentUser();
            var articles = _articleService.GetByDateRange(Mapper.Map<Common.Filters.ArticlesDateRangeFilter>(filter));
            var viewModels = articles.Select(Mapper.Map<Models.Article>).ToList();

            foreach (var article in viewModels)
            {
                article.UserHasEditAccess = currentUser.RoleId == (int) RoleType.Admin ||
                                             (currentUser.RoleId == (int) RoleType.Publisher &&
                                              currentUser.UserName == article.User.UserName);
            }

            return viewModels;
        }

        [HttpGet]
        [Route("api/Article/GetDateLimit/{categoryId}")]
        public IEnumerable<DateTime> GetDateLimit(int categoryId)
        {
            return _articleService.GetDateLimit(categoryId);
        }
            
        [HttpGet]
        [Route("api/Article/{articleId}")]
        public async Task<Models.Article> GetArticle(int articleId)
        {
            var article = await _articleService.GetArticle(articleId);
            var mappedModel = Mapper.Map<Models.Article>(article);
            var user = await GetCurrentUser();

            var currentRate = _articleRateService.GetRateByFilter(new ArticleRateFilter()
            {
                ArticleId = articleId,
                UserId = user.Id
            });

            if (currentRate != null)
            {
                mappedModel.CurrentUserRate = (int)currentRate.Value;
            }

            return mappedModel;
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Publisher)]
        [HttpPost]
        [Route("api/Article/Add")]
        public async Task<Models.Article> AddArticle(Models.Article article)
        {
            var model = Mapper.Map<Common.Article>(article);
            var user = await GetCurrentUser();
            model.UserId = user.Id;
            await _articleService.CreateArticleAsync(model);
            return Mapper.Map<Models.Article>(model);
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Publisher)]
        [HttpDelete]
        [Route("api/Article/Delete/{articleId}")]
        public async Task DeleteArticle(int articleId)
        {
            var currentUser = await GetCurrentUser();
            var article = await _articleService.GetArticle(articleId);

            if (currentUser.RoleId == (int)RoleType.Admin ||
                (currentUser.UserName == article.User.UserName))
            {
                await _articleService.DeleteArticle(article.Id);
            }
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Publisher)]
        [HttpPut]
        [Route("api/Article/Update")]
        public async Task UpdateArticle(Models.Article article)
        {
            var model = Mapper.Map<Common.Article>(article);
            var user = await GetCurrentUser();
            model.UserId = user.Id;
            await _articleService.UpdateArticle(model);
        }
    }
}
