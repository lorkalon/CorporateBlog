using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.Common.Filters;

namespace CorporateBlog.BLL.IServices
{
    public interface IArticleRateService
    {
        Task AddRate(Common.ArticleRate articleRate);
        Common.ArticleRate GetRateByFilter(ArticleRateFilter filter);

    }
}
