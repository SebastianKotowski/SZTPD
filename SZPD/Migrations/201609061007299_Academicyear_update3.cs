namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Academicyear_update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicYears", "SITerm", c => c.DateTime());
            AddColumn("dbo.AcademicYears", "NITerm", c => c.DateTime());
            AddColumn("dbo.AcademicYears", "SMTerm", c => c.DateTime());
            AddColumn("dbo.AcademicYears", "NMTerm", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcademicYears", "NMTerm");
            DropColumn("dbo.AcademicYears", "SMTerm");
            DropColumn("dbo.AcademicYears", "NITerm");
            DropColumn("dbo.AcademicYears", "SITerm");
        }
    }
}
