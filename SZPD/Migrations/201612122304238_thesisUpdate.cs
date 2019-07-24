namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thesisUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theses", "EducationProfile", c => c.String());
            DropColumn("dbo.Lecturer", "OneTimePassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lecturer", "OneTimePassword", c => c.String());
            DropColumn("dbo.Theses", "EducationProfile");
        }
    }
}
