using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class AddedPlayerStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Coaches_CoachID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CoachID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CoachID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerStatsID",
                table: "Stats");

            migrationBuilder.CreateTable(
                name: "PlayerGameStats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AVG = table.Column<decimal>(type: "TEXT", nullable: false),
                    OBP = table.Column<decimal>(type: "TEXT", nullable: false),
                    SLG = table.Column<decimal>(type: "TEXT", nullable: false),
                    OPS = table.Column<decimal>(type: "TEXT", nullable: false),
                    StatsID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGameStats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerGameStats_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerGameStats_Stats_StatsID",
                        column: x => x.StatsID,
                        principalTable: "Stats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerPosName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositions_PositionID",
                table: "PlayerPositions",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGameStats_PlayerID",
                table: "PlayerGameStats",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGameStats_StatsID",
                table: "PlayerGameStats",
                column: "StatsID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Position_PositionID",
                table: "PlayerPositions",
                column: "PositionID",
                principalTable: "Position",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Position_PositionID",
                table: "PlayerPositions");

            migrationBuilder.DropTable(
                name: "PlayerGameStats");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropIndex(
                name: "IX_PlayerPositions_PositionID",
                table: "PlayerPositions");

            migrationBuilder.AddColumn<int>(
                name: "CoachID",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerStatsID",
                table: "Stats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CoachID",
                table: "Teams",
                column: "CoachID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Coaches_CoachID",
                table: "Teams",
                column: "CoachID",
                principalTable: "Coaches",
                principalColumn: "ID");
        }
    }
}
