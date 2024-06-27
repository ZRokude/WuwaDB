using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddImageSkillTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "CharacterSkills");

            migrationBuilder.CreateTable(
                name: "Character_Skill_Image",
                columns: table => new
                {
                    CharacterSkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character_Skill_Image", x => new { x.CharacterSkillId, x.Type });
                    table.ForeignKey(
                        name: "FK_Character_Skill_Image_CharacterSkills_CharacterSkillId",
                        column: x => x.CharacterSkillId,
                        principalTable: "CharacterSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character_Skill_Image");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageFile",
                table: "CharacterSkills",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
