using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations
{
    public partial class devicemodification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetCloudURL",
                table: "Device");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetCloudURL",
                table: "Device",
                nullable: true);
        }
    }
}
