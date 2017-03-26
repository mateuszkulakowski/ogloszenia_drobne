namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BannedWordDisplayName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BannedWords", "Word", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BannedWords", "Word", c => c.String());
        }
    }
}
