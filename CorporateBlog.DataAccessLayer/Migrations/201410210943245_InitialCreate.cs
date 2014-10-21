namespace CorporateBlog.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthanticationDatas",
                c => new
                    {
                        AuthanticateDataId = c.Guid(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AuthanticateDataId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        AuthanticateDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.AuthanticationDatas", t => t.AuthanticateDataId, cascadeDelete: true)
                .Index(t => t.AuthanticateDataId);
            
            CreateTable(
                "dbo.UserPersonalDatas",
                c => new
                    {
                        UserPersonalDataId = c.Guid(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Age = c.Int(nullable: false),
                        AuthanticateDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserPersonalDataId)
                .ForeignKey("dbo.AuthanticationDatas", t => t.AuthanticateDataId)
                .Index(t => t.AuthanticateDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPersonalDatas", "AuthanticateDataId", "dbo.AuthanticationDatas");
            DropForeignKey("dbo.Blogs", "AuthanticateDataId", "dbo.AuthanticationDatas");
            DropIndex("dbo.UserPersonalDatas", new[] { "AuthanticateDataId" });
            DropIndex("dbo.Blogs", new[] { "AuthanticateDataId" });
            DropTable("dbo.UserPersonalDatas");
            DropTable("dbo.Blogs");
            DropTable("dbo.AuthanticationDatas");
        }
    }
}
