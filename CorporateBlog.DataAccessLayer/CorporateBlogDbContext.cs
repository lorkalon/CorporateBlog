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

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AuthanticationData>();

			modelBuilder.Entity<UserPersonalData>()
			            .HasRequired(t => t.AuthanticationData)
			            .WithOptional(t => t.UserPersonalData)
						.Map(k => k.MapKey("AuthanticateDataId"));

		}
	}
}
