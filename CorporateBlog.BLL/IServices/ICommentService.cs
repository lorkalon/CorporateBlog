using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.Common.Filters;

namespace CorporateBlog.BLL.IServices
{
    public interface ICommentService
    {
        Task AddComment(Common.Comment comment);
        Task UpdateComment(Common.Comment comment);
        Task DeleteComment(int commentId);

        IEnumerable<Common.Comment> GetByDateRange(CommentsDateRangeFilter filter);

    }
}
