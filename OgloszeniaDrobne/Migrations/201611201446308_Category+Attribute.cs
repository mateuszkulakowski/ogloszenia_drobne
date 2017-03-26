namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAttribute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        AttributeID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Typ = c.String(),
                    })
                .PrimaryKey(t => t.AttributeID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        IDRodzica = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
            DropTable("dbo.Attributes");
        }
    }
}
