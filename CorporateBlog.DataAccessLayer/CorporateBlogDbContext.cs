using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DataAccessLayer
{
	public class CorporateBlogDbContext:DbContext 
	{
		public CorporateBlogDbContext() : base("CorporateBlogDatabase"){}

		public DbSet<UserPersonalData> UsersPersonalsData { get; set; }
		public DbSet<AuthanticationData> AuthanticationDatas { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<LikePost> LikePosts { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<UserPersonalData>()
			            .HasRequired(t => t.AuthanticationData)
			            .WithOptional(t => t.UserPersonalData)
			            .Map(k => k.MapKey("Login"));


			modelBuilder.Entity<Category>()
						.HasRequired(t => t.Author)
						.WithMany(t=>t.Categories)
						.Map(k => k.MapKey("Login"));

			modelBuilder.Entity<Post>()
			            .HasRequired(t => t.Category)
			            .WithMany(t => t.Posts)
			            .Map(k => k.MapKey("CategoryId"));


			//modelBuilder.Entity<LikePost>()
			//			.HasRequired(t => t.Author)
			//			.WithMany(t => t.LikePosts)
			//			.Map(k => k.MapKey("Login"));

			//modelBuilder.Entity<LikePost>()
			//			.HasRequired(t => t.Post)
			//			.WithMany(t => t.Likes)
			//			.Map(k => k.MapKey("PostId"));
		}
	}
}
