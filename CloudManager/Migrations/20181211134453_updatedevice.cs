using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations
{
    public partial class updatedevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationToken",
                table: "Device");
            migrationBuilder.AddColumn<string>(
                name: "ConnectionString",
                table: "Device",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizationToken",
                table: "Device",
                nullable: true);
        }
    }
}
