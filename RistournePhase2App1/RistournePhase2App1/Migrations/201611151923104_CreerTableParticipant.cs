namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreerTableParticipant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Courriel = c.String(),
                        Rue = c.String(),
                        Ville = c.String(),
                        CodePostal = c.String(),
                        Telephone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParticipantCampagneFinancements",
                c => new
                    {
                        Participant_Id = c.Int(nullable: false),
                        CampagneFinancement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_Id, t.CampagneFinancement_Id })
                .ForeignKey("dbo.Participants", t => t.Participant_Id, cascadeDelete: true)
                .ForeignKey("dbo.CampagneFinancements", t => t.CampagneFinancement_Id, cascadeDelete: true)
                .Index(t => t.Participant_Id)
                .Index(t => t.CampagneFinancement_Id);
            
            CreateTable(
                "dbo.ParticipantOrganismes",
                c => new
                    {
                        Participant_Id = c.Int(nullable: false),
                        Organisme_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_Id, t.Organisme_Id })
                .ForeignKey("dbo.Participants", t => t.Participant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Organismes", t => t.Organisme_Id, cascadeDelete: true)
                .Index(t => t.Participant_Id)
                .Index(t => t.Organisme_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ParticipantOrganismes", new[] { "Organisme_Id" });
            DropIndex("dbo.ParticipantOrganismes", new[] { "Participant_Id" });
            DropIndex("dbo.ParticipantCampagneFinancements", new[] { "CampagneFinancement_Id" });
            DropIndex("dbo.ParticipantCampagneFinancements", new[] { "Participant_Id" });
            DropForeignKey("dbo.ParticipantOrganismes", "Organisme_Id", "dbo.Organismes");
            DropForeignKey("dbo.ParticipantOrganismes", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.ParticipantCampagneFinancements", "CampagneFinancement_Id", "dbo.CampagneFinancements");
            DropForeignKey("dbo.ParticipantCampagneFinancements", "Participant_Id", "dbo.Participants");
            DropTable("dbo.ParticipantOrganismes");
            DropTable("dbo.ParticipantCampagneFinancements");
            DropTable("dbo.Participants");
        }
    }
}
