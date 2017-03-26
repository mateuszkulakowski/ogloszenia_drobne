namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtoAdds2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ParentCategoryID", c => c.Int());
            CreateIndex("dbo.Categories", "ParentCategoryID");
            AddForeignKey("dbo.Categories", "ParentCategoryID", "dbo.Categories", "CategoryID");
            DropColumn("dbo.Categories", "IDRodzica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "IDRodzica", c => c.Int(nullable: false));
            DropForeignKey("dbo.Categories", "ParentCategoryID", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "ParentCategoryID" });
            DropColumn("dbo.Categories", "ParentCategoryID");
        }
    }
}
