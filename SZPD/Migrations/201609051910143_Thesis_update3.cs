namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thesis_update3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Theses", "CompletionDate", c => c.DateTime());
            AlterColumn("dbo.Theses", "DefenseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Theses", "DefenseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Theses", "CompletionDate", c => c.DateTime(nullable: false));
        }
    }
}
