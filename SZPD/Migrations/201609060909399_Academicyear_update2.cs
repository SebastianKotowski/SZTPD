namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Academicyear_update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcademicYears", "Year", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AcademicYears", "Year", c => c.String(nullable: false));
        }
    }
}
