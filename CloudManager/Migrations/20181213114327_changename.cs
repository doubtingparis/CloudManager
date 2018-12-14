using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations
{
    public partial class changename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastPing",
                table: "Device",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Device",
                newName: "LastPing");
        }
    }
}
