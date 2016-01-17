using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateBlog.WebApi.Authentication;

namespace CorporateBlog.WebApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public virtual WebApi.Models.UserModel User { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int ArticleId { get; set; }
        public virtual WebApi.Models.Article Article { get; set; }
        public int Rate { get; set; }
        public bool CanBeEditedByUser { get; set; }
        public int UserVotedRate { get; set; }
    }
}