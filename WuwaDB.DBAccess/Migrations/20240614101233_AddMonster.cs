using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddMonster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_Type_Name",
                table: "CharacterSkills");

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonsterStatsGrowthProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    LifeMaxRatio = table.Column<int>(type: "int", nullable: false),
                    AtkRatio = table.Column<int>(type: "int", nullable: false),
                    DefRatio = table.Column<int>(type: "int", nullable: false),
                    HardnessMaxRatio = table.Column<int>(type: "int", nullable: false),
                    HardnessRatio = table.Column<int>(type: "int", nullable: false),
                    HardnessRecoverRatio = table.Column<int>(type: "int", nullable: false),
                    RageMaxRatio = table.Column<int>(type: "int", nullable: false),
                    RageRatio = table.Column<int>(type: "int", nullable: false),
                    RageRecoverRatio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterStatsGrowthProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonsterStatsBases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonsterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    ATK = table.Column<int>(type: "int", nullable: false),
                    DEF = table.Column<int>(type: "int", nullable: false),
                    Hardness = table.Column<int>(type: "int", nullable: false),
                    Rage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterStatsBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonsterStatsBases_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monster_Stats_Base_Ele_Res",
                columns: table => new
                {
                    MonsterStatsBaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElementResist = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monster_Stats_Base_Ele_Res", x => new { x.MonsterStatsBaseId, x.ElementResist });
                    table.ForeignKey(
                        name: "FK_Monster_Stats_Base_Ele_Res_MonsterStatsBases_MonsterStatsBaseId",
                        column: x => x.MonsterStatsBaseId,
                        principalTable: "MonsterStatsBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_Type_Name",
                table: "CharacterSkills",
                columns: new[] { "Type", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_Name",
                table: "Monsters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonsterStatsBases_MonsterId",
                table: "MonsterStatsBases",
                column: "MonsterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monster_Stats_Base_Ele_Res");

            migrationBuilder.DropTable(
                name: "MonsterStatsGrowthProperties");

            migrationBuilder.DropTable(
                name: "MonsterStatsBases");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_Type_Name",
                table: "CharacterSkills");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_Type_Name",
                table: "CharacterSkills",
                columns: new[] { "Type", "Name" });
        }
    }
}
