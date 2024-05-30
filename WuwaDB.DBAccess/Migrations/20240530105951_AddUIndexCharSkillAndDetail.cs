using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUIndexCharSkillAndDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_CharacterId_Type",
                table: "CharacterSkills");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkillDetails_CharacterSkillId",
                table: "CharacterSkillDetails");

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionName",
                table: "CharacterSkills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SkillDetailsName",
                table: "CharacterSkillDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterId_Type_DescriptionName",
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "Type", "DescriptionName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillDetails_CharacterSkillId_SkillDetailsName",
                table: "CharacterSkillDetails",
                columns: new[] { "CharacterSkillId", "SkillDetailsName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_CharacterId_Type_DescriptionName",
                table: "CharacterSkills");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkillDetails_CharacterSkillId_SkillDetailsName",
                table: "CharacterSkillDetails");

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionName",
                table: "CharacterSkills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SkillDetailsName",
                table: "CharacterSkillDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterId_Type",
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillDetails_CharacterSkillId",
                table: "CharacterSkillDetails",
                column: "CharacterSkillId");
        }
    }
}
