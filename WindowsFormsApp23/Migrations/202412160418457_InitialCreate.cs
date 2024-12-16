using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Data.Entity;


namespace WindowsFormsApp23.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faculty",
                c => new
                {
                    FacultyID = c.Int(nullable: false, identity: true),
                    FacultyName = c.String(nullable: false, maxLength: 200),
                })
                .PrimaryKey(t => t.FacultyID);

            CreateTable(
                "dbo.Student",
                c => new
                {
                    StudentID = c.String(nullable: false, maxLength: 20),
                    FullName = c.String(nullable: false, maxLength: 200),
                    AverageScore = c.Double(),
                    FacultyID = c.Int(),
                })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Faculty", t => t.FacultyID)
                .Index(t => t.FacultyID);

            // Thêm dữ liệu mặc định
            Sql("INSERT INTO Faculty (FacultyID, FacultyName) VALUES (1, N'Khoa Công Nghệ Thông Tin')");
            Sql("INSERT INTO Faculty (FacultyID, FacultyName) VALUES (2, N'Khoa Kinh Tế')");
            Sql("INSERT INTO Faculty (FacultyID, FacultyName) VALUES (3, N'Khoa Luật')");
        }

    }
}
