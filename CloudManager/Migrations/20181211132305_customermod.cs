using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudManager.Migrations
{
    public partial class customermod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfDevices",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfDevices",
                table: "Customer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
