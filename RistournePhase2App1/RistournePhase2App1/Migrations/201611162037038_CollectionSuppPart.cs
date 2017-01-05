namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollectionSuppPart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Participants", "Supporteur_Id", "dbo.Supporteurs");
            DropIndex("dbo.Participants", new[] { "Supporteur_Id" });
            CreateTable(
                "dbo.ParticipantSupporteurs",
                c => new
                    {
                        Participant_Id = c.Int(nullable: false),
                        Supporteur_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_Id, t.Supporteur_Id })
                .ForeignKey("dbo.Participants", t => t.Participant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Supporteurs", t => t.Supporteur_Id, cascadeDelete: true)
                .Index(t => t.Participant_Id)
                .Index(t => t.Supporteur_Id);
            
            DropColumn("dbo.Participants", "Supporteur_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "Supporteur_Id", c => c.Int());
            DropIndex("dbo.ParticipantSupporteurs", new[] { "Supporteur_Id" });
            DropIndex("dbo.ParticipantSupporteurs", new[] { "Participant_Id" });
            DropForeignKey("dbo.ParticipantSupporteurs", "Supporteur_Id", "dbo.Supporteurs");
            DropForeignKey("dbo.ParticipantSupporteurs", "Participant_Id", "dbo.Participants");
            DropTable("dbo.ParticipantSupporteurs");
            CreateIndex("dbo.Participants", "Supporteur_Id");
            AddForeignKey("dbo.Participants", "Supporteur_Id", "dbo.Supporteurs", "Id");
        }
    }
}
