using System.Collections.Generic;

namespace CorporateBlog.DAL.Models
{
    public class Comment:BaseEntity
    {
        public string Text { get; set; }
        
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public virtual ICollection<CommentRate> CommentRates { get; set; } 
    }
}
