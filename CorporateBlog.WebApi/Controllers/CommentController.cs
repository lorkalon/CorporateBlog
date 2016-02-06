using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.DAL.Models;
using CorporateBlog.WebApi.Authentication;

namespace CorporateBlog.WebApi.Controllers
{
    [ExtendedAuthorize]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly ICommentRateService _commentRateService;

        public CommentController(
            ICommentService commentService, 
            ApplicationUserManager userManager,
            ICommentRateService commentRateService):base(userManager)
        {
            _commentService = commentService;
            _commentRateService = commentRateService;
        }

        [HttpPost]
        [Route("api/Comment/Add")]
        public async Task AddComment(Models.Comment comment)
        {
            var mappedComment = Mapper.Map<Common.Comment>(comment);
            mappedComment.UserId = (await GetCurrentUser()).Id;
            await _commentService.AddComment(mappedComment);
        }

        [HttpDelete]
        [Route("api/Comment/Delete/{commentId}")]
        public async Task DeleteComment(int commentId)
        {
            var savedComment = await _commentService.GetById(commentId);
            var currentUser = await GetCurrentUser();

            if (currentUser.RoleName == RoleType.Admin || 
                savedComment.UserId == currentUser.Id)
            {
                await _commentService.DeleteComment(commentId);
            }
        }

        [HttpGet]
        [Route("api/Comment/GetByFilter")]
        public async Task<IEnumerable<Models.Comment>> GetComments([FromUri] Models.Filters.CommentsFilter filter)
        {
            var currentUser = await GetCurrentUser();
            var comments =
                _commentService.GetCommentsByFilter(Mapper.Map<Common.Filters.CommentsFilter>(filter))
                    .Select(Mapper.Map<Models.Comment>)
                    .ToList();

            foreach (var comment in comments)
            {
                comment.CanBeEditedByUser = currentUser.RoleId == (int) RoleType.Admin ||
                                            comment.User.Id == currentUser.Id;

                var rate = _commentRateService.FindCommentRate(comment.Id, currentUser.Id);

                if (rate != null)
                {
                    comment.UserVotedRate = rate.Value;
                }
            }

            return comments;
        }

        [HttpPost]
        [Route("api/Comment/Vote")]
        public async Task RateForComment(Models.CommentRate rate)
        {
            if (rate.Value != RateType.Like && rate.Value != RateType.Dislike)
            {
                throw new ArgumentException("Rate is not valid!");
            }

            var user = await GetCurrentUser();
            rate.UserId = user.Id;

            var model = Mapper.Map<Common.CommentRate>(rate);
            await _commentRateService.AddRate(model);
        }

    }
}
