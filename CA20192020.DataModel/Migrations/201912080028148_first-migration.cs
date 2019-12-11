namespace CA20192020.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnrollmentApplications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(maxLength: 128),
                        ModuleID = c.Int(nullable: false),
                        DateApplied = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .Index(t => t.StudentID)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        ModuleCode = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(maxLength: 128),
                        ModuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .Index(t => t.StudentID)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.Lecturers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModuleAssessments",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                        PercentageAllocation = c.Single(nullable: false),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Modules", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.ModuleAttendances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleID = c.Int(nullable: false),
                        StudentID = c.String(maxLength: 128),
                        StartDateTime = c.DateTime(nullable: false),
                        FinishDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .Index(t => t.ModuleID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.ModuleDeliveries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleID = c.Int(nullable: false),
                        LecturerID = c.Int(nullable: false),
                        DeliveryDay = c.Int(nullable: false),
                        DeliveryTime = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lecturers", t => t.LecturerID, cascadeDelete: true)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .Index(t => t.ModuleID)
                .Index(t => t.LecturerID);
            
            CreateTable(
                "dbo.StudentGrades",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AssessmentID = c.Int(nullable: false),
                        StudentID = c.String(maxLength: 128),
                        Grade = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ModuleAssessments", t => t.AssessmentID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .Index(t => t.AssessmentID)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGrades", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentGrades", "AssessmentID", "dbo.ModuleAssessments");
            DropForeignKey("dbo.ModuleDeliveries", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.ModuleDeliveries", "LecturerID", "dbo.Lecturers");
            DropForeignKey("dbo.ModuleAttendances", "StudentID", "dbo.Students");
            DropForeignKey("dbo.ModuleAttendances", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.ModuleAssessments", "ID", "dbo.Modules");
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.EnrollmentApplications", "StudentID", "dbo.Students");
            DropForeignKey("dbo.EnrollmentApplications", "ModuleID", "dbo.Modules");
            DropIndex("dbo.StudentGrades", new[] { "StudentID" });
            DropIndex("dbo.StudentGrades", new[] { "AssessmentID" });
            DropIndex("dbo.ModuleDeliveries", new[] { "LecturerID" });
            DropIndex("dbo.ModuleDeliveries", new[] { "ModuleID" });
            DropIndex("dbo.ModuleAttendances", new[] { "StudentID" });
            DropIndex("dbo.ModuleAttendances", new[] { "ModuleID" });
            DropIndex("dbo.ModuleAssessments", new[] { "ID" });
            DropIndex("dbo.Enrollments", new[] { "ModuleID" });
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            DropIndex("dbo.EnrollmentApplications", new[] { "ModuleID" });
            DropIndex("dbo.EnrollmentApplications", new[] { "StudentID" });
            DropTable("dbo.StudentGrades");
            DropTable("dbo.ModuleDeliveries");
            DropTable("dbo.ModuleAttendances");
            DropTable("dbo.ModuleAssessments");
            DropTable("dbo.Lecturers");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Students");
            DropTable("dbo.Modules");
            DropTable("dbo.EnrollmentApplications");
        }
    }
}
