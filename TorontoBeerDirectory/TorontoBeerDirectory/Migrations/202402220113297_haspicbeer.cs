namespace TorontoBeerDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class haspicbeer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "BeerHasPic", c => c.Boolean(nullable: false));
            AddColumn("dbo.Beers", "PicExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "PicExtension");
            DropColumn("dbo.Beers", "BeerHasPic");
        }
    }
}
