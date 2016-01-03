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
        private readonly ApplicationUserManager _userManager;

        public ArticleController(
            IArticleService articleService, 
            IArticleRateService articleRateService, 
            ApplicationUserManager userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
            _articleRateService = articleRateService;
        }

        [HttpGet]
        [Route("api/Article/GetByDateRange")]
        public IEnumerable<Models.Article> GetArticles([FromUri]ArticlesDateRangeFilter filter)
        {
            var userName = User.Identity.GetUserName();
            var articles = _articleService.GetByDateRange(Mapper.Map<Common.Filters.ArticlesDateRangeFilter>(filter));
            var viewModels = articles.Select(Mapper.Map<Models.Article>).ToList();

            foreach (var article in viewModels)
            {
                article.UserHasEditAccess = article.User.RoleId == (int) RoleType.Admin ||
                                             (article.User.RoleId == (int) RoleType.Publisher &&
                                              article.User.UserName == userName);
            }

            return viewModels;
        }

        [HttpGet]
        [Route("api/Article/GetDateLimit")]
        public IEnumerable<DateTime> GetDateLimit()
        {
            return _articleService.GetDateLimit();
        }
            
        [HttpGet]
        [Route("api/Article/{articleId}")]
        public async Task<Models.Article> GetArticle(int articleId)
        {
            var article = await _articleService.GetArticle(articleId);
            var mappedModel = Mapper.Map<Models.Article>(article);
            var userName = User.Identity.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);

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
            var userName = User.Identity.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);
            model.UserId = user.Id;
            await _articleService.CreateArticleAsync(model);
            return Mapper.Map<Models.Article>(model);
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Publisher)]
        [HttpDelete]
        [Route("api/Article/Delete/{articleId}")]
        public async Task DeleteArticle(int articleId)
        {
            var userName = User.Identity.GetUserName();
            var article = await _articleService.GetArticle(articleId);

            if (article.User.RoleId == (int) RoleType.Admin ||
                (article.User.RoleId == (int) RoleType.Publisher &&
                 article.User.UserName == userName))
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
            var userName = User.Identity.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);
            model.UserId = user.Id;
            await _articleService.UpdateArticle(model);
        }
    }
}
