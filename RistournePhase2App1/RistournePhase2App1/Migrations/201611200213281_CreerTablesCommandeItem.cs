namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreerTablesCommandeItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        ModePaiement = c.String(nullable: false),
                        DateLimite = c.DateTime(nullable: false),
                        DateLivraison = c.DateTime(nullable: false),
                        DateCree = c.DateTime(nullable: false),
                        CreePar = c.String(),
                        LieuLivraison = c.String(nullable: false),
                        Supporteur_Id = c.Int(),
                        Campagnes_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supporteurs", t => t.Supporteur_Id)
                .ForeignKey("dbo.CampagneFinancements", t => t.Campagnes_Id)
                .Index(t => t.Supporteur_Id)
                .Index(t => t.Campagnes_Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Quantite = c.Int(nullable: false),
                        Montant = c.Int(nullable: false),
                        Produit_Id = c.Int(),
                        Commande_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produits", t => t.Produit_Id)
                .ForeignKey("dbo.Commandes", t => t.Commande_Id)
                .Index(t => t.Produit_Id)
                .Index(t => t.Commande_Id);
            
            CreateTable(
                "dbo.CampagneFinancementSupporteurs",
                c => new
                    {
                        CampagneFinancement_Id = c.Int(nullable: false),
                        Supporteur_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CampagneFinancement_Id, t.Supporteur_Id })
                .ForeignKey("dbo.CampagneFinancements", t => t.CampagneFinancement_Id, cascadeDelete: true)
                .ForeignKey("dbo.Supporteurs", t => t.Supporteur_Id, cascadeDelete: true)
                .Index(t => t.CampagneFinancement_Id)
                .Index(t => t.Supporteur_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CampagneFinancementSupporteurs", new[] { "Supporteur_Id" });
            DropIndex("dbo.CampagneFinancementSupporteurs", new[] { "CampagneFinancement_Id" });
            DropIndex("dbo.Items", new[] { "Commande_Id" });
            DropIndex("dbo.Items", new[] { "Produit_Id" });
            DropIndex("dbo.Commandes", new[] { "Campagnes_Id" });
            DropIndex("dbo.Commandes", new[] { "Supporteur_Id" });
            DropForeignKey("dbo.CampagneFinancementSupporteurs", "Supporteur_Id", "dbo.Supporteurs");
            DropForeignKey("dbo.CampagneFinancementSupporteurs", "CampagneFinancement_Id", "dbo.CampagneFinancements");
            DropForeignKey("dbo.Items", "Commande_Id", "dbo.Commandes");
            DropForeignKey("dbo.Items", "Produit_Id", "dbo.Produits");
            DropForeignKey("dbo.Commandes", "Campagnes_Id", "dbo.CampagneFinancements");
            DropForeignKey("dbo.Commandes", "Supporteur_Id", "dbo.Supporteurs");
            DropTable("dbo.CampagneFinancementSupporteurs");
            DropTable("dbo.Items");
            DropTable("dbo.Commandes");
        }
    }
}
