using CorporateBlog.BLL.IServices;
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
    }
}
