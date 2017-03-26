namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackToUser2withRequredPools : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adds", "User_UserID", "dbo.Users");
            DropIndex("dbo.Adds", new[] { "User_UserID" });
            AddColumn("dbo.Adds", "Author", c => c.String());
            DropColumn("dbo.Adds", "User_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Adds", "User_UserID", c => c.Int());
            DropColumn("dbo.Adds", "Author");
            CreateIndex("dbo.Adds", "User_UserID");
            AddForeignKey("dbo.Adds", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
