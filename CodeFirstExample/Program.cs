using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstExample
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new BloggingContext())
			{
				var blog = new Blog() { Name = "Hello" };
				db.Blogs.Add(blog);

				var post = new Post() {Content = "post content"};
				db.Posts.Add(post);

				db.SaveChanges();

				var query = from b in db.Blogs select b.Posts;


				foreach (var item in query)
				{
					foreach (var p in item)
					{
						Console.WriteLine(p.Content);
					}
				}

				Console.ReadKey();
			}	
		}
	}
}
