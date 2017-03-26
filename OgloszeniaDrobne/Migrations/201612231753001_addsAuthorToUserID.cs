namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsAuthorToUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adds", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Adds", "UserID");
            AddForeignKey("dbo.Adds", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            DropColumn("dbo.Adds", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Adds", "Author", c => c.String());
            DropForeignKey("dbo.Adds", "UserID", "dbo.Users");
            DropIndex("dbo.Adds", new[] { "UserID" });
            DropColumn("dbo.Adds", "UserID");
        }
    }
}
