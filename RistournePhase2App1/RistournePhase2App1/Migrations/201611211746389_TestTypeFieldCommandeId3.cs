namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestTypeFieldCommandeId3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "CommandeId", "dbo.Commandes");
            DropIndex("dbo.Items", new[] { "CommandeId" });
            AlterColumn("dbo.Items", "CommandeId", c => c.Int(nullable: true));
            AddForeignKey("dbo.Items", "CommandeId", "dbo.Commandes", "Id", cascadeDelete: true);
            CreateIndex("dbo.Items", "CommandeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "CommandeId" });
            DropForeignKey("dbo.Items", "CommandeId", "dbo.Commandes");
            AlterColumn("dbo.Items", "CommandeId", c => c.Int());
            CreateIndex("dbo.Items", "CommandeId");
            AddForeignKey("dbo.Items", "CommandeId", "dbo.Commandes", "Id");
        }
    }
}
