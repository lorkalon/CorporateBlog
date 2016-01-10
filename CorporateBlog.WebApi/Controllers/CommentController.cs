using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.Models;
using CorporateBlog.WebApi.Authentication;
using CorporateBlog.WebApi.Models.Filters;

namespace CorporateBlog.WebApi.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        public CommentController(
            ICommentService commentService, 
            ApplicationUserManager userManager):base(userManager)
        {
            _commentService = commentService;
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
                comment.CanBeEditedByUser = comment.User.RoleId == (int) RoleType.Admin ||
                                            comment.User.Id == currentUser.Id;
            }

            return comments;
        }

    }
}
