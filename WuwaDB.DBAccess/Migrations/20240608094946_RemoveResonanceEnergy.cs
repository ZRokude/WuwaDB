using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveResonanceEnergy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Max_Resonance_Energy",
                table: "CharacterStatsBases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Max_Resonance_Energy",
                table: "CharacterStatsBases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
