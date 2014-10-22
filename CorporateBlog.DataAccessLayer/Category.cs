using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DataAccessLayer
{
	public class Category
	{
		[Key]
		public Guid CategoryId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		[Required]
		public virtual AuthanticationData Author { get; set; }
		
		public virtual List<Post> Posts { get; set; } 
	}
}
