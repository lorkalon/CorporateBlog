using System.Collections;
using System.Collections.Generic;
using CorporateBlog.Common.Filters;

namespace CorporateBlog.BLL.IServices
{
    public interface IArticleService
    {
        void CreateArticle();

        IEnumerable<Common.Article> GetPaged(BaseFilter filter);
    }
}
