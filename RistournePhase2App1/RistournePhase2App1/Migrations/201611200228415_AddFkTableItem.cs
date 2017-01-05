namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFkTableItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Produit_Id", "dbo.Produits");
            DropIndex("dbo.Items", new[] { "Produit_Id" });
            RenameColumn(table: "dbo.Items", name: "Produit_Id", newName: "ProduitId");
            AddForeignKey("dbo.Items", "ProduitId", "dbo.Produits", "Id", cascadeDelete: true);
            CreateIndex("dbo.Items", "ProduitId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "ProduitId" });
            DropForeignKey("dbo.Items", "ProduitId", "dbo.Produits");
            RenameColumn(table: "dbo.Items", name: "ProduitId", newName: "Produit_Id");
            CreateIndex("dbo.Items", "Produit_Id");
            AddForeignKey("dbo.Items", "Produit_Id", "dbo.Produits", "Id");
        }
    }
}
