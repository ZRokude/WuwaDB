using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangesCharacterEntityStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "ImageFile",
                table: "Characters",
                newName: "ImageModel");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageCard",
                table: "Characters",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCard",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "ImageModel",
                table: "Characters",
                newName: "ImageFile");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
