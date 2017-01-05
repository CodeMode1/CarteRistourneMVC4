namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreerTableOrganisme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organismes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Rue = c.String(),
                        Ville = c.Int(nullable: false),
                        CodePostal = c.String(),
                        Telephone = c.String(),
                        Telecopieur = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Organismes");
        }
    }
}
