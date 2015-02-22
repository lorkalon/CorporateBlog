using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class CommentRateRepository:GenericRepository<Common.CommentRate, CommentRate>, ICommentRateRepository
    {
        public CommentRateRepository(DbContext context) : base(context)
        {
        }
    }
}
