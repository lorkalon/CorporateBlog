using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using AutoMapper;
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
                        .HasOptional(b => b.UserInfo)
                        .WithRequired(info => info.User);

            modelBuilder.Entity<User>()
                        .Property(user => user.Login)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.Password)
                        .IsRequired();

            modelBuilder.Entity<Article>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.Articles)
                        .HasForeignKey(d => d.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                        .HasRequired(t => t.Category)
                        .WithMany(t => t.Articles)
                        .HasForeignKey(d => d.CategoryId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.Comments)
                        .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Comment>()
                        .HasRequired(t => t.Article)
                        .WithMany(t => t.Comments)
                        .HasForeignKey(t => t.ArticleId);

            modelBuilder.Entity<CommentRate>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.CommentRates)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommentRate>()
                        .HasRequired(t => t.Comment)
                        .WithMany(t => t.CommentRates)
                        .HasForeignKey(t => t.CommentId);

            modelBuilder.Entity<ArticleRate>()
                        .HasRequired(t => t.User)
                        .WithMany(t => t.ArticleRates)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArticleRate>()
                        .HasRequired(t => t.Article)
                        .WithMany(t => t.ArticleRates)
                        .HasForeignKey(t => t.ArticleId);


        }
    }
}
