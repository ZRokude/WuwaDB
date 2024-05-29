using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGrowthProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Skill_Details_CharacterSkills_CharacterSkillId",
                table: "Character_Skill_Details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Character_Skill_Details",
                table: "Character_Skill_Details");

            migrationBuilder.RenameTable(
                name: "Character_Skill_Details",
                newName: "CharacterSkillDetails");

            migrationBuilder.RenameIndex(
                name: "IX_Character_Skill_Details_CharacterSkillId",
                table: "CharacterSkillDetails",
                newName: "IX_CharacterSkillDetails_CharacterSkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterSkillDetails",
                table: "CharacterSkillDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CharacterStatsGrowthproperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    BreachLevel = table.Column<int>(type: "int", nullable: false),
                    LifeMaxRatio = table.Column<int>(type: "int", nullable: false),
                    AtkRatio = table.Column<int>(type: "int", nullable: false),
                    DefRatio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterStatsGrowthproperties", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkillDetails_CharacterSkills_CharacterSkillId",
                table: "CharacterSkillDetails",
                column: "CharacterSkillId",
                principalTable: "CharacterSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkillDetails_CharacterSkills_CharacterSkillId",
                table: "CharacterSkillDetails");

            migrationBuilder.DropTable(
                name: "CharacterStatsGrowthproperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterSkillDetails",
                table: "CharacterSkillDetails");

            migrationBuilder.RenameTable(
                name: "CharacterSkillDetails",
                newName: "Character_Skill_Details");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterSkillDetails_CharacterSkillId",
                table: "Character_Skill_Details",
                newName: "IX_Character_Skill_Details_CharacterSkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Character_Skill_Details",
                table: "Character_Skill_Details",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Skill_Details_CharacterSkills_CharacterSkillId",
                table: "Character_Skill_Details",
                column: "CharacterSkillId",
                principalTable: "CharacterSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
