namespace TorontoBeerDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class breweries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Breweries",
                c => new
                    {
                        BreweryID = c.Int(nullable: false, identity: true),
                        BreweryName = c.String(),
                        BreweryLocation = c.String(),
                    })
                .PrimaryKey(t => t.BreweryID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Breweries");
        }
    }
}
