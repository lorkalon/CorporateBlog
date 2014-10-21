namespace CorporateBlog.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuthanticationDatas", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.AuthanticationDatas", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AuthanticationDatas", "Password", c => c.String());
            AlterColumn("dbo.AuthanticationDatas", "Email", c => c.String());
        }
    }
}
