using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.DbContextProvider
{
    public class CorporateBlogContext : DbContext
    {
        public CorporateBlogContext()
            : base("CorporateBlogContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentRate> CommentRates { get; set; }
        public DbSet<ArticleRate> ArticleRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                        .HasKey(e => e.Id)
                        .Property(e => e.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<User>()
                        .Property(user => user.UserName)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.PasswordHash)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.RoleId)
                        .IsRequired();

            modelBuilder.Entity<UserInfo>()
                        .HasKey(info => info.UserId);

            modelBuilder.Entity<User>()
                        .HasOptional(m => m.UserInfo)
                        .WithRequired(info => info.User);


            modelBuilder.Entity<Category>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.Categories)
                        .HasForeignKey(category => category.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.Articles)
                        .HasForeignKey(d => d.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                        .HasRequired(t => t.Category)
                        .WithMany(t => t.Articles)
                        .HasForeignKey(d => d.CategoryId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.Comments)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                        .HasRequired(t => t.Article)
                        .WithMany(t => t.Comments)
                        .HasForeignKey(t => t.ArticleId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<CommentRate>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.CommentRates)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommentRate>()
                        .HasRequired(t => t.Comment)
                        .WithMany(t => t.CommentRates)
                        .HasForeignKey(t => t.CommentId)
                        .WillCascadeOnDelete(true);


            modelBuilder.Entity<ArticleRate>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.ArticleRates)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArticleRate>()
                        .HasRequired(t => t.Article)
                        .WithMany(t => t.ArticleRates)
                        .HasForeignKey(t => t.ArticleId)
                        .WillCascadeOnDelete(true);

        }
    }
}
