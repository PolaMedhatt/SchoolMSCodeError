using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjects",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjects", x => new { x.StudentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "CS101", "Learn cyber security basics", "Cyber Security" },
                    { 2, "UI201", "Learn UI and UX design", "UI/UX" },
                    { 3, "MOD301", "Modify and improve code", "Code Modification" },
                    { 4, "EMB401", "Intro to embedded systems", "Embedded Systems" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Email", "Name", "Specialization" },
                values: new object[,]
                {
                    { 1, "ahmed@gmail.com", "Dalia", "Computer Science" },
                    { 2, "mohamed@gmail.com", "Khaled", "IT" },
                    { 3, "khaled@gmail.com", "Yara", "Software Eng" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Capacity", "Grade", "Name", "TeacherId" },
                values: new object[,]
                {
                    { 1, 20, "1", "s1", 1 },
                    { 2, 20, "2", "s2", 2 },
                    { 3, 20, "3", "s3", 2 },
                    { 4, 20, "4", "s4", 2 },
                    { 5, 20, "1", "w1", 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassId", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "ahmed@student.com", "Ahmed", "01000000001" },
                    { 2, 2, "mohamed@student.com", "Mohamed", "01000000002" },
                    { 3, 3, "khaled@student.com", "Khaled", "01000000003" },
                    { 4, 4, "asia@student.com", "Asia", "01000000004" },
                    { 5, 1, "ashraqat@student.com", "Ashraqat", "01000000005" }
                });

            migrationBuilder.InsertData(
                table: "StudentProfiles",
                columns: new[] { "Id", "Address", "DateOfBirth", "StudentId" },
                values: new object[,]
                {
                    { 1, "Cairo", new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Alex", new DateTime(2004, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Giza", new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Sharqia", new DateTime(2002, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.InsertData(
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 3 },
                    { 3, 4 },
                    { 4, 3 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Name",
                table: "Classes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_StudentId",
                table: "StudentProfiles",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Code",
                table: "Subjects",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProfiles");

            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
