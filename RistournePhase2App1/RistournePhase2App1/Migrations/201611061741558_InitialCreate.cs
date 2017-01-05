namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supporteurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        MotPasse = c.String(nullable: false, maxLength: 100),
                        Courriel = c.String(nullable: false),
                        Rue = c.String(),
                        Ville = c.String(),
                        CodePostal = c.String(),
                        Telephone = c.String(),
                        ChoixQuestion = c.String(),
                        ReponseQuestion = c.String(),
                        SouvenirMoi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Supporteurs");
        }
    }
}
