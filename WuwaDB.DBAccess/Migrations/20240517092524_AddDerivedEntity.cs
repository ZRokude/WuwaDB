using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDerivedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkillValues_CharacterSkillId",
                table: "CharacterSkillValues");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CharacterSkills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillValues_CharacterSkillId_SkillValueNumber",
                table: "CharacterSkillValues",
                columns: new[] { "CharacterSkillId", "SkillValueNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_Name",
                table: "CharacterSkills",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                table: "Characters",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkillValues_CharacterSkillId_SkillValueNumber",
                table: "CharacterSkillValues");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_Name",
                table: "CharacterSkills");

            migrationBuilder.DropIndex(
                name: "IX_Characters_Name",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Email",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Username",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CharacterSkills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillValues_CharacterSkillId",
                table: "CharacterSkillValues",
                column: "CharacterSkillId");
        }
    }
}
