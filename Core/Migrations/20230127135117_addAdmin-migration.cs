using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class addAdminmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO AspNetRoles VALUES ( NEWID(),'User','USER',NEWID())" + 
          $"INSERT INTO AspNetRoles VALUES(NEWID(), 'Admin', 'ADMIN', NEWID())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
