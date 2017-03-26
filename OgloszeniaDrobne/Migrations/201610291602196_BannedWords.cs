namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BannedWords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannedWords",
                c => new
                    {
                        BannedWordID = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                    })
                .PrimaryKey(t => t.BannedWordID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BannedWords");
        }
    }
}
