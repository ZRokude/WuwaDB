using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillDetailsNum",
                table: "CharacterSkillDetails");

            migrationBuilder.CreateTable(
                name: "CharacterSkillDetailNumbers",
                columns: table => new
                {
                    CharacterSkillDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkillDetailNumbers", x => new { x.CharacterSkillDetailId, x.Level });
                    table.ForeignKey(
                        name: "FK_CharacterSkillDetailNumbers_CharacterSkillDetails_CharacterSkillDetailId",
                        column: x => x.CharacterSkillDetailId,
                        principalTable: "CharacterSkillDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkillDetailNumbers");

            migrationBuilder.AddColumn<double>(
                name: "SkillDetailsNum",
                table: "CharacterSkillDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
