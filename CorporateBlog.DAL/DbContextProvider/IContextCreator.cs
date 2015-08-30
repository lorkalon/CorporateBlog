using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DAL.DbContextProvider
{
    public interface IContextCreator
    {
        DbContext GetContext { get; }
    }
}
