using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;

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
        [Route("api/Article/GetFiltered")]
        public IEnumerable<Models.Article> GetArticles(Models.Filters.BaseFilter filter)
        {
            //var articles = _articleService.GetFiltered(Mapper.Map<Common.Filters.BaseFilter>(filter));
            //return articles.Select(Mapper.Map<Models.Article>);
            return null;
        } 

    }
}
