namespace TorontoBeerDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beeralcoholcontent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "BeerAlcoholContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "BeerAlcoholContent");
        }
    }
}
