using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChernabogJailApp.Migrations
{
    public partial class BeastVariations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeastVariation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    BeastId = table.Column<int>(type: "integer", nullable: false),
                    HitDice = table.Column<int>(type: "integer", nullable: false),
                    ArmorClass = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<string>(type: "text", nullable: false),
                    Damage = table.Column<string>(type: "text", nullable: false),
                    Movement = table.Column<int>(type: "integer", nullable: false),
                    Fly = table.Column<int>(type: "integer", nullable: false),
                    Swim = table.Column<int>(type: "integer", nullable: false),
                    Teleport = table.Column<int>(type: "integer", nullable: false),
                    BattleSpirit = table.Column<int>(type: "integer", nullable: false),
                    SaveRoll = table.Column<int>(type: "integer", nullable: false),
                    Skill = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeastVariation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeastVariation_Beast_BeastId",
                        column: x => x.BeastId,
                        principalTable: "Beast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeastVariation_BeastId",
                table: "BeastVariation",
                column: "BeastId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeastVariation");
        }
    }
}
