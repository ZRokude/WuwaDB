using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterSkillDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_CharacterId_Type_DescriptionName",
                table: "CharacterSkills");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CharacterSkills");

            migrationBuilder.DropColumn(
                name: "DescriptionName",
                table: "CharacterSkills");

            migrationBuilder.CreateTable(
                name: "CharacterSkillDescriptions",
                columns: table => new
                {
                    CharacterSkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescriptionTItle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkillDescriptions", x => new { x.CharacterSkillId, x.DescriptionTItle });
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterId_Type",
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkillDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_CharacterId_Type",
                table: "CharacterSkills");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CharacterSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionName",
                table: "CharacterSkills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterId_Type_DescriptionName",
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "Type", "DescriptionName" },
                unique: true);
        }
    }
}
