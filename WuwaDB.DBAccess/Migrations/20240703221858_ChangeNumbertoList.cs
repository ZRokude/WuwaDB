using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNumbertoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Multiplier",
                table: "CharacterSkillDetailNumbers");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CharacterSkillDetailNumbers");

            migrationBuilder.CreateTable(
                name: "NumberMultipliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<double>(type: "float", nullable: false),
                    Multiplier = table.Column<int>(type: "int", nullable: true),
                    CharacterSkillDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberMultipliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumberMultipliers_CharacterSkillDetailNumbers_CharacterSkillDetailId_Level",
                        columns: x => new { x.CharacterSkillDetailId, x.Level },
                        principalTable: "CharacterSkillDetailNumbers",
                        principalColumns: new[] { "CharacterSkillDetailId", "Level" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberMultipliers_CharacterSkillDetailId_Level",
                table: "NumberMultipliers",
                columns: new[] { "CharacterSkillDetailId", "Level" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberMultipliers");

            migrationBuilder.AddColumn<int>(
                name: "Multiplier",
                table: "CharacterSkillDetailNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Number",
                table: "CharacterSkillDetailNumbers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
