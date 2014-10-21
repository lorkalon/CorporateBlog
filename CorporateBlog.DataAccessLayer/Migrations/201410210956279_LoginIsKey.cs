namespace CorporateBlog.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginIsKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "AuthanticateDataId", "dbo.AuthanticationDatas");
            DropForeignKey("dbo.UserPersonalDatas", "AuthanticateDataId", "dbo.AuthanticationDatas");
            DropIndex("dbo.Blogs", new[] { "AuthanticateDataId" });
            DropIndex("dbo.UserPersonalDatas", new[] { "AuthanticateDataId" });
            DropPrimaryKey("dbo.AuthanticationDatas");
            AddColumn("dbo.AuthanticationDatas", "Email", c => c.String());
            AlterColumn("dbo.AuthanticationDatas", "Login", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Blogs", "AuthanticateDataId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserPersonalDatas", "AuthanticateDataId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AuthanticationDatas", "Login");
            CreateIndex("dbo.Blogs", "AuthanticateDataId");
            CreateIndex("dbo.UserPersonalDatas", "AuthanticateDataId");
            AddForeignKey("dbo.Blogs", "AuthanticateDataId", "dbo.AuthanticationDatas", "Login", cascadeDelete: true);
            AddForeignKey("dbo.UserPersonalDatas", "AuthanticateDataId", "dbo.AuthanticationDatas", "Login");
            DropColumn("dbo.AuthanticationDatas", "AuthanticateDataId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthanticationDatas", "AuthanticateDataId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.UserPersonalDatas", "AuthanticateDataId", "dbo.AuthanticationDatas");
            DropForeignKey("dbo.Blogs", "AuthanticateDataId", "dbo.AuthanticationDatas");
            DropIndex("dbo.UserPersonalDatas", new[] { "AuthanticateDataId" });
            DropIndex("dbo.Blogs", new[] { "AuthanticateDataId" });
            DropPrimaryKey("dbo.AuthanticationDatas");
            AlterColumn("dbo.UserPersonalDatas", "AuthanticateDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Blogs", "AuthanticateDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AuthanticationDatas", "Login", c => c.String());
            DropColumn("dbo.AuthanticationDatas", "Email");
            AddPrimaryKey("dbo.AuthanticationDatas", "AuthanticateDataId");
            CreateIndex("dbo.UserPersonalDatas", "AuthanticateDataId");
            CreateIndex("dbo.Blogs", "AuthanticateDataId");
            AddForeignKey("dbo.UserPersonalDatas", "AuthanticateDataId", "dbo.AuthanticationDatas", "Login");
            AddForeignKey("dbo.Blogs", "AuthanticateDataId", "dbo.AuthanticationDatas", "Login", cascadeDelete: true);
        }
    }
}
