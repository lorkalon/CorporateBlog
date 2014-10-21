using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstExample
{
	public class Post
	{
		public int PostId { get; set; }
	
		public string Name { get; set; }
		public int BlogId { get; set; }
		public string Content { get; set; }

		public virtual Blog Blog { get; set; }
		
	}
}
