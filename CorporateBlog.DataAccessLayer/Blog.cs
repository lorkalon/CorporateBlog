using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DataAccessLayer
{
	public class Blog
	{
		[Key]
		public Guid BlogId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		[Required]
		public virtual AuthanticationData Blogger { get; set; }
	}
}
