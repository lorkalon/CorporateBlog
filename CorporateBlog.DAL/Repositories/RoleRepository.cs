using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.DAL.Repositories
{
    public class RoleRepository:GenericRepository<DAL.Models.Role>, IRoleRepository
    {
        public RoleRepository(IContextCreator contextCreator) : base(contextCreator)
        {
        }
    }
}
