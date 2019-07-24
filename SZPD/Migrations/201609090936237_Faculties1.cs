namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Faculties1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theses", "FacultyId", c => c.Int(nullable: false));
            AddColumn("dbo.Theses", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.Theses", "SpecializationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Theses", "SpecializationId");
            DropColumn("dbo.Theses", "CourseId");
            DropColumn("dbo.Theses", "FacultyId");
        }
    }
}
