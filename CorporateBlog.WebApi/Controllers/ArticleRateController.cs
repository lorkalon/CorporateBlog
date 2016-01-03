using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.WebApi.Authentication;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Controllers
{
    [Authorize]
    public class ArticleRateController : BaseController
    {
        private readonly IArticleRateService _articleRateService;
        private readonly ApplicationUserManager _userManager;

        public ArticleRateController(IArticleRateService articleRateService, ApplicationUserManager userManager)
        {
            _articleRateService = articleRateService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("api/ArticleRate/Vote")]
        public async Task VoteForArticle([FromBody]Models.ArticleRate articleRate)
        {
            if (articleRate.Value!= RateType.Like && 
                articleRate.Value != RateType.Dislike)
            {
                throw new Exception("Rate value is not valid!");
            }
            var userName = User.Identity.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);
            articleRate.UserId = user.Id;
            var mapped = Mapper.Map<Common.ArticleRate>(articleRate);
            await _articleRateService.AddRate(mapped);
        }
    }
}
