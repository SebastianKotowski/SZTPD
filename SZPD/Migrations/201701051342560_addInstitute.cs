namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addInstitute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Institutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Theses", "InstituteId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Theses", "InstituteId");
            DropTable("dbo.Institutes");
        }
    }
}
