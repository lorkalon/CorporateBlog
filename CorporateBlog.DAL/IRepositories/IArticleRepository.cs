using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IArticleRepository : IGenericRepository<Common.Article, Article>
    {
    }
}
