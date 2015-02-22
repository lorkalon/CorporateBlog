using System.Collections.Generic;

namespace CorporateBlog.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Salt { get; set; }

        public int? UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentRate> CommentRates { get; set; } 
        public virtual ICollection<ArticleRate> ArticleRates { get; set; } 
 
    }
}
