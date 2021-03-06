﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CorporateBlog.DAL.Models
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public string Email { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public int RoleId { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Blocked { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentRate> CommentRates { get; set; } 
        public virtual ICollection<ArticleRate> ArticleRates { get; set; } 
 
    }
}
