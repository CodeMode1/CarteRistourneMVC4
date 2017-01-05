namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FkCommandeTableItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Commande_Id", "dbo.Commandes");
            DropIndex("dbo.Items", new[] { "Commande_Id" });
            RenameColumn(table: "dbo.Items", name: "Commande_Id", newName: "CommandeId");
            AddForeignKey("dbo.Items", "CommandeId", "dbo.Commandes", "Id", cascadeDelete: true);
            CreateIndex("dbo.Items", "CommandeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "CommandeId" });
            DropForeignKey("dbo.Items", "CommandeId", "dbo.Commandes");
            RenameColumn(table: "dbo.Items", name: "CommandeId", newName: "Commande_Id");
            CreateIndex("dbo.Items", "Commande_Id");
            AddForeignKey("dbo.Items", "Commande_Id", "dbo.Commandes", "Id");
        }
    }
}
