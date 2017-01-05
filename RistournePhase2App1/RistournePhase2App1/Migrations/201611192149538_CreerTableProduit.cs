namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreerTableProduit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Rabais = c.Int(nullable: false),
                        PrixUnitaire = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Produits");
        }
    }
}
