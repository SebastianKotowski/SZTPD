namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcademicYear_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicYears", "LimitSI", c => c.Int(nullable: false));
            AddColumn("dbo.AcademicYears", "LimitNI", c => c.Int(nullable: false));
            AddColumn("dbo.AcademicYears", "LimitSM", c => c.Int(nullable: false));
            AddColumn("dbo.AcademicYears", "LimitNM", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcademicYears", "LimitNM");
            DropColumn("dbo.AcademicYears", "LimitSM");
            DropColumn("dbo.AcademicYears", "LimitNI");
            DropColumn("dbo.AcademicYears", "LimitSI");
        }
    }
}
