using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DAL.DbContextProvider
{
    public class CorporateBlogContextCreator:IContextCreator
    {
        private readonly CorporateBlogContext _context;

        public CorporateBlogContextCreator()
        {
            _context = new CorporateBlogContext();
        }

        public DbContext GetContext
        {
            get { return _context; }
        }
    }
}
