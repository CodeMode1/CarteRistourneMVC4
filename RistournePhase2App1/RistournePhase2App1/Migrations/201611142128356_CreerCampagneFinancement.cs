namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreerCampagneFinancement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CampagneFinancements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Organisme_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organismes", t => t.Organisme_Id)
                .Index(t => t.Organisme_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CampagneFinancements", new[] { "Organisme_Id" });
            DropForeignKey("dbo.CampagneFinancements", "Organisme_Id", "dbo.Organismes");
            DropTable("dbo.CampagneFinancements");
        }
    }
}
