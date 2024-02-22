namespace TorontoBeerDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class haspicbrewery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Breweries", "BreweryHasPic", c => c.Boolean(nullable: false));
            AddColumn("dbo.Breweries", "PicExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Breweries", "PicExtension");
            DropColumn("dbo.Breweries", "BreweryHasPic");
        }
    }
}
