namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsUserUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adds", "User_UserID", c => c.Int());
            CreateIndex("dbo.Adds", "User_UserID");
            AddForeignKey("dbo.Adds", "User_UserID", "dbo.Users", "UserID");
            DropColumn("dbo.Adds", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Adds", "Author", c => c.String());
            DropForeignKey("dbo.Adds", "User_UserID", "dbo.Users");
            DropIndex("dbo.Adds", new[] { "User_UserID" });
            DropColumn("dbo.Adds", "User_UserID");
        }
    }
}
