using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationDescWithSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkillDescriptions_CharacterSkills_CharacterSkillId",
                table: "CharacterSkillDescriptions",
                column: "CharacterSkillId",
                principalTable: "CharacterSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkillDescriptions_CharacterSkills_CharacterSkillId",
                table: "CharacterSkillDescriptions");
        }
    }
}
