using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixSkillDetailRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkillPerformLevels");

            migrationBuilder.DropTable(
                name: "CharacterSkillPerforms");

            migrationBuilder.DropColumn(
                name: "Max_Stamina",
                table: "CharacterStatsBases");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CharacterSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionName",
                table: "CharacterSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CharacterSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Character_Skill_Details",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterSkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillDetailsNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillDetailsName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character_Skill_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Skill_Details_CharacterSkills_CharacterSkillId",
                        column: x => x.CharacterSkillId,
                        principalTable: "CharacterSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_Skill_Details_CharacterSkillId",
                table: "Character_Skill_Details",
                column: "CharacterSkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character_Skill_Details");

            migrationBuilder.DropColumn(
                name: "DescriptionName",
                table: "CharacterSkills");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CharacterSkills");

            migrationBuilder.AddColumn<int>(
                name: "Max_Stamina",
                table: "CharacterStatsBases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CharacterSkills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CharacterSkillPerforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterSkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkillPerforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterSkillPerforms_CharacterSkills_CharacterSkillId",
                        column: x => x.CharacterSkillId,
                        principalTable: "CharacterSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkillPerformLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterSkillPerformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkillPerformLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterSkillPerformLevels_CharacterSkillPerforms_CharacterSkillPerformId",
                        column: x => x.CharacterSkillPerformId,
                        principalTable: "CharacterSkillPerforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillPerformLevels_CharacterSkillPerformId",
                table: "CharacterSkillPerformLevels",
                column: "CharacterSkillPerformId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillPerforms_CharacterSkillId",
                table: "CharacterSkillPerforms",
                column: "CharacterSkillId");
        }
    }
}
