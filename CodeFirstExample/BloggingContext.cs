using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeFirstExample
{
	public class BloggingContext:DbContext
	{
		public BloggingContext():base("SomeDB"){}

		public DbSet<Post> Posts { get; set; }
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
