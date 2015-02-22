using System.Collections.Generic;

namespace CorporateBlog.DAL.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ArticleRate> ArticleRates { get; set; } 
        public virtual ICollection<Comment> Comments { get; set; } 

    }
}
