namespace TorontoBeerDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beers",
                c => new
                    {
                        BeerID = c.Int(nullable: false, identity: true),
                        BeerName = c.String(),
                        BeerType = c.String(),
                        BeerDescription = c.String(),
                    })
                .PrimaryKey(t => t.BeerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Beers");
        }
    }
}
