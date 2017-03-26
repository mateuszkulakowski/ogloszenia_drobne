namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users2withRequiredPools : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Adds", "TelephoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Adds", "TelephoneNumber", c => c.String());
        }
    }
}
