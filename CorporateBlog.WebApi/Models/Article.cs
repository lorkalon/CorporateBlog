using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateBlog.WebApi.Authentication;

namespace CorporateBlog.WebApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual WebApi.Models.UserModel User { get; set; }
        public virtual WebApi.Models.Category Category { get; set; }
        public int Rate { get; set; }

        public bool UserHasEditAccess { get; set; }
        public int CurrentUserRate { get; set; }
    }
}