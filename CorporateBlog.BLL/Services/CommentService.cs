using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.Common.Filters;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.BLL.Services
{
    public class CommentService:BaseService, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(IContextProvider contextProvider, ICommentRepository commentRepository) : base(contextProvider)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddComment(Common.Comment comment)
        {
            var entity = Mapper.Map<DAL.Models.Comment>(comment);
            _commentRepository.Add(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateComment(Common.Comment comment)
        {
            var entity = await _commentRepository.GetAsync(comment.Id);

            if (entity != null)
            {
                entity.Text = comment.Text;
                await SaveChangesAsync();
            }
        }

        public async Task DeleteComment(int commentId)
        {
            var entity = await _commentRepository.GetAsync(commentId);
            
            if (entity != null)
            {
               _commentRepository.Delete(entity);
                await SaveChangesAsync();
            }
        }

        public IEnumerable<Comment> GetByDateRange(CommentsDateRangeFilter filter)
        {
            Expression<Func<DAL.Models.Comment, bool>> whereExpressions = a => a.ArticleId == filter.ArticleId &&
                                                                               a.CreatedOnUtc >= filter.StartDate &&
                                                                               a.CreatedOnUtc <= filter.EndDate;


            Expression<Func<DAL.Models.Comment, object>> orderBy = comment => comment.CreatedOnUtc;

            var comments =
                _commentRepository.GetFiltered(whereExpressions, orderBy, null, null, false)
                    .Select(Mapper.Map<Common.Comment>)
                    .ToList();

            return comments;
        }
    }
}
