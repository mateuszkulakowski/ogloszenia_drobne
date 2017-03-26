namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToAddsCategoriesAndAdd_Atr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddAtrributes",
                c => new
                    {
                        AddAttributeID = c.Int(nullable: false, identity: true),
                        AddID = c.Int(nullable: false),
                        AttributeID = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.AddAttributeID)
                .ForeignKey("dbo.Adds", t => t.AddID, cascadeDelete: false)
                .ForeignKey("dbo.Attributes", t => t.AttributeID, cascadeDelete: false)
                .Index(t => t.AddID)
                .Index(t => t.AttributeID);
            
            AddColumn("dbo.Adds", "CategoryID", c => c.Int(nullable: true));
            CreateIndex("dbo.Adds", "CategoryID");
            AddForeignKey("dbo.Adds", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddAtrributes", "AttributeID", "dbo.Attributes");
            DropForeignKey("dbo.AddAtrributes", "AddID", "dbo.Adds");
            DropForeignKey("dbo.Adds", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Adds", new[] { "CategoryID" });
            DropIndex("dbo.AddAtrributes", new[] { "AttributeID" });
            DropIndex("dbo.AddAtrributes", new[] { "AddID" });
            DropColumn("dbo.Adds", "CategoryID");
            DropTable("dbo.AddAtrributes");
        }
    }
}
