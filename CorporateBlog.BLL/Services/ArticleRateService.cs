using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.DAL.DbContextProvider;

namespace CorporateBlog.BLL.Services
{
    public class ArticleRateService:BaseService, IArticleRateService
    {
        public ArticleRateService(IContextProvider contextProvider) : base(contextProvider)
        {
        }

        public void AddRate(Common.ArticleRate articleRate)
        {
            throw new NotImplementedException();
        }
    }
}
