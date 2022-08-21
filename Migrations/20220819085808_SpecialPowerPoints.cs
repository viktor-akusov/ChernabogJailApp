using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChernabogJailApp.Migrations
{
    public partial class SpecialPowerPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "SpecialPower",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "SpecialPower");
        }
    }
}
