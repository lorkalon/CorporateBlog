using System.Collections.Generic;

namespace CorporateBlog.DAL.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Article> Articles { get; set; } 
    }
}
