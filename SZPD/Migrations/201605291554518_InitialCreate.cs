namespace SZPD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Theses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        EnglishSubject = c.String(nullable: false),
                        Objective = c.String(nullable: false),
                        Tasks = c.String(nullable: false),
                        Comment = c.String(),
                        PrivateComment = c.String(),
                        Status = c.String(nullable: false),
                        Version = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Student = c.String(),
                        CompletionDate = c.DateTime(nullable: false),
                        DefenseDate = c.DateTime(nullable: false),
                        Rate = c.Single(nullable: false),
                        LecturerID = c.String(maxLength: 128),
                        AcademicYearID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearID, cascadeDelete: true)
                .ForeignKey("dbo.Lecturer", t => t.LecturerID)
                .Index(t => t.LecturerID)
                .Index(t => t.AcademicYearID);
            
            CreateTable(
                "dbo.Lecturer",
                c => new
                    {
                        Primary_key = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        Lastname = c.String(),
                        Password = c.String(),
                        OneTimePassword = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        EmailLinkDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        AddThesisPermision = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Primary_key)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        UserClaim = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaim)
                .ForeignKey("dbo.Lecturer", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Lecturer", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Lecturer", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Theses", "LecturerID", "dbo.Lecturer");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Lecturer");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.Lecturer");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.Lecturer");
            DropForeignKey("dbo.Theses", "AcademicYearID", "dbo.AcademicYears");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.Lecturer", "UserNameIndex");
            DropIndex("dbo.Theses", new[] { "AcademicYearID" });
            DropIndex("dbo.Theses", new[] { "LecturerID" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.Lecturer");
            DropTable("dbo.Theses");
            DropTable("dbo.AcademicYears");
        }
    }
}
