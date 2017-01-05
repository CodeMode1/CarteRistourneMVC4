namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutImgUrlProduit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produits", "imageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produits", "imageUrl");
        }
    }
}
