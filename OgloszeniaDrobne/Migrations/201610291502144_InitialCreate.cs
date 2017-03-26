namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adds",
                c => new
                    {
                        AddID = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        TelephoneNumber = c.String(),
                        Price = c.Double(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AddID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Adds");
        }
    }
}
