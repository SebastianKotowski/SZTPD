namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkPass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lecturer", "LinkPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lecturer", "LinkPassword");
        }
    }
}
