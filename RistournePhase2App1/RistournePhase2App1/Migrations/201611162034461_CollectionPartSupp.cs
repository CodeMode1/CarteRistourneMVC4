namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollectionPartSupp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParticipantCampagneFinancements", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.ParticipantCampagneFinancements", "CampagneFinancement_Id", "dbo.CampagneFinancements");
            DropForeignKey("dbo.ParticipantOrganismes", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.ParticipantOrganismes", "Organisme_Id", "dbo.Organismes");
            DropIndex("dbo.ParticipantCampagneFinancements", new[] { "Participant_Id" });
            DropIndex("dbo.ParticipantCampagneFinancements", new[] { "CampagneFinancement_Id" });
            DropIndex("dbo.ParticipantOrganismes", new[] { "Participant_Id" });
            DropIndex("dbo.ParticipantOrganismes", new[] { "Organisme_Id" });
            CreateTable(
                "dbo.OrganismeParticipants",
                c => new
                    {
                        Organisme_Id = c.Int(nullable: false),
                        Participant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Organisme_Id, t.Participant_Id })
                .ForeignKey("dbo.Organismes", t => t.Organisme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.Participant_Id, cascadeDelete: true)
                .Index(t => t.Organisme_Id)
                .Index(t => t.Participant_Id);
            
            CreateTable(
                "dbo.CampagneFinancementParticipants",
                c => new
                    {
                        CampagneFinancement_Id = c.Int(nullable: false),
                        Participant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CampagneFinancement_Id, t.Participant_Id })
                .ForeignKey("dbo.CampagneFinancements", t => t.CampagneFinancement_Id, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.Participant_Id, cascadeDelete: true)
                .Index(t => t.CampagneFinancement_Id)
                .Index(t => t.Participant_Id);
            
            AddColumn("dbo.Participants", "Supporteur_Id", c => c.Int());
            AddForeignKey("dbo.Participants", "Supporteur_Id", "dbo.Supporteurs", "Id");
            CreateIndex("dbo.Participants", "Supporteur_Id");
            DropTable("dbo.ParticipantCampagneFinancements");
            DropTable("dbo.ParticipantOrganismes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ParticipantOrganismes",
                c => new
                    {
                        Participant_Id = c.Int(nullable: false),
                        Organisme_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_Id, t.Organisme_Id });
            
            CreateTable(
                "dbo.ParticipantCampagneFinancements",
                c => new
                    {
                        Participant_Id = c.Int(nullable: false),
                        CampagneFinancement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_Id, t.CampagneFinancement_Id });
            
            DropIndex("dbo.CampagneFinancementParticipants", new[] { "Participant_Id" });
            DropIndex("dbo.CampagneFinancementParticipants", new[] { "CampagneFinancement_Id" });
            DropIndex("dbo.OrganismeParticipants", new[] { "Participant_Id" });
            DropIndex("dbo.OrganismeParticipants", new[] { "Organisme_Id" });
            DropIndex("dbo.Participants", new[] { "Supporteur_Id" });
            DropForeignKey("dbo.CampagneFinancementParticipants", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.CampagneFinancementParticipants", "CampagneFinancement_Id", "dbo.CampagneFinancements");
            DropForeignKey("dbo.OrganismeParticipants", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.OrganismeParticipants", "Organisme_Id", "dbo.Organismes");
            DropForeignKey("dbo.Participants", "Supporteur_Id", "dbo.Supporteurs");
            DropColumn("dbo.Participants", "Supporteur_Id");
            DropTable("dbo.CampagneFinancementParticipants");
            DropTable("dbo.OrganismeParticipants");
            CreateIndex("dbo.ParticipantOrganismes", "Organisme_Id");
            CreateIndex("dbo.ParticipantOrganismes", "Participant_Id");
            CreateIndex("dbo.ParticipantCampagneFinancements", "CampagneFinancement_Id");
            CreateIndex("dbo.ParticipantCampagneFinancements", "Participant_Id");
            AddForeignKey("dbo.ParticipantOrganismes", "Organisme_Id", "dbo.Organismes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParticipantOrganismes", "Participant_Id", "dbo.Participants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParticipantCampagneFinancements", "CampagneFinancement_Id", "dbo.CampagneFinancements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParticipantCampagneFinancements", "Participant_Id", "dbo.Participants", "Id", cascadeDelete: true);
        }
    }
}
