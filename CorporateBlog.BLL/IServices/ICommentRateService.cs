using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.BLL.IServices
{
    public interface ICommentRateService
    {
        Task AddRate(Common.CommentRate rate);
        Common.CommentRate FindCommentRate(int commentId, int userId);
    }
}
