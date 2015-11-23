using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.Common.Filters;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        IEnumerable<Article> GetPaged(BaseFilter filter);
    }
}
