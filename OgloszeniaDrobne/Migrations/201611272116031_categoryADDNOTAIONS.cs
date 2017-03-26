namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryADDNOTAIONS : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Nazwa", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Nazwa", c => c.String());
        }
    }
}
