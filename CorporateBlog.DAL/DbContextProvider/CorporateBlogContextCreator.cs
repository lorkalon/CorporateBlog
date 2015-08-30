using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.DbContextProvider
{
    public class CorporateBlogContextCreator:IContextCreator
    {
        private readonly CorporateBlogContext _context;

        public CorporateBlogContextCreator()
        {
            _context = new CorporateBlogContext();
            SetDefaultRoles();
        }

        public DbContext GetContext
        {
            get { return _context; }
        }

        private void SetDefaultRoles()
        {
            if (!_context.Roles.Any())
            {
                _context.Roles.Add(new Role()
                {
                    Id = (int) RoleType.Admin,
                    Name = "Admin"
                });

                _context.Roles.Add(new Role()
                {
                    Id = (int) RoleType.Publisher,
                    Name = "Publisher"
                });

                _context.Roles.Add(new Role()
                {
                    Id = (int) RoleType.Client,
                    Name = "Client"
                });

                _context.SaveChanges();
            }
        }
    }
}
