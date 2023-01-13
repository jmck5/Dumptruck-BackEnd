using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dumptruck_v4.Migrations
{
    public partial class notesScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Notes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Notes");
        }
    }
}
