using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Common.Article> GetPaged(BaseFilter filter)
        {
            var articles = _articleRepository.GetPaged(filter);
            return articles.Select(Mapper.Map<Common.Article>);
        }
    }
}
