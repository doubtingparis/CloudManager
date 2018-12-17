using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations.IdentityDB
{
    public partial class redefine_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminRole",
                table: "AspNetUsers",
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
