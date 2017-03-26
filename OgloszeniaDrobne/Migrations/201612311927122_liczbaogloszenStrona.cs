namespace OgloszeniaDrobne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class liczbaogloszenStrona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IloscOgloszenStrona", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IloscOgloszenStrona");
        }
    }
}
