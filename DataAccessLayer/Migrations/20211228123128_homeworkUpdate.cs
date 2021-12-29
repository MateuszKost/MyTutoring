using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class homeworkUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Homework_HomeworkId",
                table: "Material");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_HomeworkId",
                table: "Material",
                column: "HomeworkId",
                principalTable: "Homework",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_HomeworkId",
                table: "Material");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Homework_HomeworkId",
                table: "Material",
                column: "HomeworkId",
                principalTable: "Homework",
                principalColumn: "Id");
        }
    }
}
