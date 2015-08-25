using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class CommentRepository:GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IContextCreator contextCreator) : base(contextCreator)
        {
        }
    }
}
