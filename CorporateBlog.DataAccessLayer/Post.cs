using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DataAccessLayer
{
	public class Post
	{
		[Key]
		public Guid PostId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }

		public virtual Category Category { get; set; }
	}
}
