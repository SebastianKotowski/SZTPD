namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thesis_update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theses", "ThesisLocation", c => c.String());
            DropColumn("dbo.Theses", "PlaceOfWork");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Theses", "PlaceOfWork", c => c.String());
            DropColumn("dbo.Theses", "ThesisLocation");
        }
    }
}
