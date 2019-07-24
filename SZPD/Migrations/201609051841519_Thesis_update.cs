namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thesis_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theses", "Committee", c => c.String());
            AddColumn("dbo.Theses", "InputData", c => c.String());
            AddColumn("dbo.Theses", "ThesisRange", c => c.String());
            AddColumn("dbo.Theses", "PlaceOfWork", c => c.String());
            AddColumn("dbo.Theses", "Settled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Theses", "Settled");
            DropColumn("dbo.Theses", "PlaceOfWork");
            DropColumn("dbo.Theses", "ThesisRange");
            DropColumn("dbo.Theses", "InputData");
            DropColumn("dbo.Theses", "Committee");
        }
    }
}
