using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.WebApi.Models.Filters;

namespace CorporateBlog.WebApi.Controllers
{
    [Authorize]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("api/Article/GetByDateRange")]
        public IEnumerable<Models.Article> GetArticles([FromUri]ArticlesDateRangeFilter filter)
        {
            var articles = _articleService.GetByDateRange(Mapper.Map<Common.Filters.ArticlesDateRangeFilter>(filter));
            return articles.Select(Mapper.Map<Models.Article>);
        }

        [HttpPost]
        [Route("api/Article/Add")]
        public void AddArticle(Models.Article article)
        {
            
        } 

    }
}
