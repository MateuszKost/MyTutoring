using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class DBTableNamesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_TeacherId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Homework_TeacherId",
                table: "Homework");

            migrationBuilder.DropTable(
                name: "StudentTeacher");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Homework",
                newName: "TutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Homework_TeacherId",
                table: "Homework",
                newName: "IX_Homework_TutorId");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Activity",
                newName: "TutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_TeacherId",
                table: "Activity",
                newName: "IX_Activity_TutorId");

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Tutor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTutor",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTutor", x => new { x.StudentId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_StudentTutor_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_StudentTutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTutor_TutorId",
                table: "StudentTutor",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_TutorId",
                table: "Activity",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_TutorId",
                table: "Homework",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_TutorId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Homework_TutorId",
                table: "Homework");

            migrationBuilder.DropTable(
                name: "StudentTutor");

            migrationBuilder.DropTable(
                name: "Tutor");

            migrationBuilder.RenameColumn(
                name: "TutorId",
                table: "Homework",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Homework_TutorId",
                table: "Homework",
                newName: "IX_Homework_TeacherId");

            migrationBuilder.RenameColumn(
                name: "TutorId",
                table: "Activity",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Activity_TutorId",
                table: "Activity",
                newName: "IX_Activity_TeacherId");

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Teacher_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTeacher",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTeacher", x => new { x.StudentId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_StudentTeacher_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_StudentTeacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacher_TeacherId",
                table: "StudentTeacher",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_TeacherId",
                table: "Activity",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_TeacherId",
                table: "Homework",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "UserId");
        }
    }
}
