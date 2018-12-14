using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations
{
    public partial class removeconnectionstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionString",
                table: "Device");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectionString",
                table: "Device",
                nullable: true);
        }
    }
}
