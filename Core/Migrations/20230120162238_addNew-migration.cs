using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class addNewmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Requirement",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Requirement",
                table: "Jobs");
        }
    }
}
