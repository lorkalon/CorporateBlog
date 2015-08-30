using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL;
using CorporateBlog.DAL.DbContextProvider;

namespace CorporateBlog.BLL.Services
{
    public abstract class BaseService
    {
        private readonly IContextProvider _contextProvider;

        protected BaseService(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        protected void SaveChanges()
        {
            _contextProvider.SaveChanges();
        }
    }
}
