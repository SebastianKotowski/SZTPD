namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class model_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theses", "ReviewerCommittee", c => c.String());
            AddColumn("dbo.Theses", "ChairmanCommittee", c => c.String());
            AddColumn("dbo.Specializations", "Stationary", c => c.Boolean(nullable: false));
            AddColumn("dbo.Specializations", "Extramural", c => c.Boolean(nullable: false));
            DropColumn("dbo.Theses", "Committee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Theses", "Committee", c => c.String());
            DropColumn("dbo.Specializations", "Extramural");
            DropColumn("dbo.Specializations", "Stationary");
            DropColumn("dbo.Theses", "ChairmanCommittee");
            DropColumn("dbo.Theses", "ReviewerCommittee");
        }
    }
}
