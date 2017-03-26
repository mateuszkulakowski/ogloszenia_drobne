namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Imie", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Nazwisko", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Haslo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Haslo", c => c.String());
            AlterColumn("dbo.Users", "Login", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Nazwisko", c => c.String());
            AlterColumn("dbo.Users", "Imie", c => c.String());
        }
    }
}
