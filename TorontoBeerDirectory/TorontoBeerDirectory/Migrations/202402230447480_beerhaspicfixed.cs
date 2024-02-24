namespace TorontoBeerDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beerhaspicfixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "BreweryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "BreweryName");
        }
    }
}
