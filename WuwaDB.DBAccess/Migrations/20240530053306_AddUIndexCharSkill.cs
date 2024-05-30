using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUIndexCharSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_CharacterId",
                table: "CharacterSkills");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterId_Type",
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_CharacterId_Type",
                table: "CharacterSkills");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterId",
                table: "CharacterSkills",
                column: "CharacterId");
        }
    }
}
