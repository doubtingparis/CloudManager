using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations
{
    public partial class deviceauthkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthKey",
                table: "Device",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthKey",
                table: "Device");
        }
    }
}
