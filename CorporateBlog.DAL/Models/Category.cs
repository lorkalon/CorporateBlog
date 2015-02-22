using System.Collections.Generic;

namespace CorporateBlog.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Article> Articles { get; set; } 
    }
}
