using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCharSkillId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CharacterSkillId",
                table: "CharacterSkillDetailNumbers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Character_SkillId",
                table: "CharacterSkillDetailNumbers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillDetailNumbers_Character_SkillId",
                table: "CharacterSkillDetailNumbers",
                column: "Character_SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkillDetailNumbers_CharacterSkills_Character_SkillId",
                table: "CharacterSkillDetailNumbers",
                column: "Character_SkillId",
                principalTable: "CharacterSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkillDetailNumbers_CharacterSkills_Character_SkillId",
                table: "CharacterSkillDetailNumbers");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkillDetailNumbers_Character_SkillId",
                table: "CharacterSkillDetailNumbers");

            migrationBuilder.DropColumn(
                name: "CharacterSkillId",
                table: "CharacterSkillDetailNumbers");

            migrationBuilder.DropColumn(
                name: "Character_SkillId",
                table: "CharacterSkillDetailNumbers");
        }
    }
}
