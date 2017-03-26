namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryatrdic2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Dictionaries", "Attribute_AttributeID", "dbo.Attributes");
            DropForeignKey("dbo.Attributes", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Attributes", new[] { "Category_CategoryID" });
            DropIndex("dbo.Dictionaries", new[] { "Attribute_AttributeID" });
            DropIndex("dbo.Categories", new[] { "Category_CategoryID" });
            RenameColumn(table: "dbo.Dictionaries", name: "Attribute_AttributeID", newName: "AttributeID");
            RenameColumn(table: "dbo.Attributes", name: "Category_CategoryID", newName: "CategoryID");
            AlterColumn("dbo.Attributes", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Dictionaries", "AttributeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Attributes", "CategoryID");
            CreateIndex("dbo.Dictionaries", "AttributeID");
            AddForeignKey("dbo.Dictionaries", "AttributeID", "dbo.Attributes", "AttributeID", cascadeDelete: true);
            AddForeignKey("dbo.Attributes", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            DropColumn("dbo.Categories", "Category_CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Category_CategoryID", c => c.Int());
            DropForeignKey("dbo.Attributes", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Dictionaries", "AttributeID", "dbo.Attributes");
            DropIndex("dbo.Dictionaries", new[] { "AttributeID" });
            DropIndex("dbo.Attributes", new[] { "CategoryID" });
            AlterColumn("dbo.Dictionaries", "AttributeID", c => c.Int());
            AlterColumn("dbo.Attributes", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.Attributes", name: "CategoryID", newName: "Category_CategoryID");
            RenameColumn(table: "dbo.Dictionaries", name: "AttributeID", newName: "Attribute_AttributeID");
            CreateIndex("dbo.Categories", "Category_CategoryID");
            CreateIndex("dbo.Dictionaries", "Attribute_AttributeID");
            CreateIndex("dbo.Attributes", "Category_CategoryID");
            AddForeignKey("dbo.Attributes", "Category_CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.Dictionaries", "Attribute_AttributeID", "dbo.Attributes", "AttributeID");
            AddForeignKey("dbo.Categories", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
