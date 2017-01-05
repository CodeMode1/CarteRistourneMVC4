namespace RistournePhase2App1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringVilleTableOrganisme : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organismes", "Ville", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organismes", "Ville", c => c.Int(nullable: false));
        }
    }
}
