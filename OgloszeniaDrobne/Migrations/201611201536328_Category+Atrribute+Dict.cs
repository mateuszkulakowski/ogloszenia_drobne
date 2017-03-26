namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAtrributeDict : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        DictionaryID = c.Int(nullable: false, identity: true),
                        Pole = c.String(),
                        Attribute_AttributeID = c.Int(),
                    })
                .PrimaryKey(t => t.DictionaryID)
                .ForeignKey("dbo.Attributes", t => t.Attribute_AttributeID)
                .Index(t => t.Attribute_AttributeID);
            
            AddColumn("dbo.Attributes", "Category_CategoryID", c => c.Int());
            AddColumn("dbo.Categories", "Category_CategoryID", c => c.Int());
            CreateIndex("dbo.Attributes", "Category_CategoryID");
            CreateIndex("dbo.Categories", "Category_CategoryID");
            AddForeignKey("dbo.Attributes", "Category_CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.Categories", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Attributes", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Dictionaries", "Attribute_AttributeID", "dbo.Attributes");
            DropIndex("dbo.Categories", new[] { "Category_CategoryID" });
            DropIndex("dbo.Dictionaries", new[] { "Attribute_AttributeID" });
            DropIndex("dbo.Attributes", new[] { "Category_CategoryID" });
            DropColumn("dbo.Categories", "Category_CategoryID");
            DropColumn("dbo.Attributes", "Category_CategoryID");
            DropTable("dbo.Dictionaries");
        }
    }
}
