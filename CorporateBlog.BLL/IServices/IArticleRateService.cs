using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.BLL.IServices
{
    public interface IArticleRateService
    {
        void AddRate(Common.ArticleRate articleRate);
    }
}
