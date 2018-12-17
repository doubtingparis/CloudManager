using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations.IdentityDB
{
    public partial class revertadmincolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminRole",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
