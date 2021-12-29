using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class TaskSolutionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSha1",
                table: "TaskSolution");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TaskSolution",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "TaskSolution",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "FileSha1",
                table: "TaskSolution",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
