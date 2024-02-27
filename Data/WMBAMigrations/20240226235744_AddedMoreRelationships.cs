using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class AddedMoreRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGameStats_Players_PlayerID",
                table: "PlayerGameStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGameStats_Stats_StatsID",
                table: "PlayerGameStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Position_PositionID",
                table: "PlayerPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Position",
                table: "Position");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGameStats",
                table: "PlayerGameStats");

            migrationBuilder.RenameTable(
                name: "Position",
                newName: "Positions");

            migrationBuilder.RenameTable(
                name: "PlayerGameStats",
                newName: "PlayerGamesStats");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGameStats_StatsID",
                table: "PlayerGamesStats",
                newName: "IX_PlayerGamesStats_StatsID");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGameStats_PlayerID",
                table: "PlayerGamesStats",
                newName: "IX_PlayerGamesStats_PlayerID");

            migrationBuilder.AddColumn<int>(
                name: "PlayerStatsID",
                table: "Stats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGamesStats",
                table: "PlayerGamesStats",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PA = table.Column<int>(type: "INTEGER", nullable: false),
                    Runs = table.Column<int>(type: "INTEGER", nullable: false),
                    Hits = table.Column<int>(type: "INTEGER", nullable: false),
                    B1 = table.Column<int>(type: "INTEGER", nullable: false),
                    B2 = table.Column<int>(type: "INTEGER", nullable: false),
                    B3 = table.Column<int>(type: "INTEGER", nullable: false),
                    HR = table.Column<int>(type: "INTEGER", nullable: false),
                    RBI = table.Column<int>(type: "INTEGER", nullable: false),
                    BB = table.Column<int>(type: "INTEGER", nullable: false),
                    K = table.Column<int>(type: "INTEGER", nullable: false),
                    SB = table.Column<int>(type: "INTEGER", nullable: false),
                    SAC = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerStats_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerStatsID",
                table: "Stats",
                column: "PlayerStatsID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositions_PlayerID",
                table: "PlayerPositions",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_PlayerID",
                table: "PlayerStats",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGamesStats_Players_PlayerID",
                table: "PlayerGamesStats",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGamesStats_Stats_StatsID",
                table: "PlayerGamesStats",
                column: "StatsID",
                principalTable: "Stats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Players_PlayerID",
                table: "PlayerPositions",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Positions_PositionID",
                table: "PlayerPositions",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_PlayerStats_PlayerStatsID",
                table: "Stats",
                column: "PlayerStatsID",
                principalTable: "PlayerStats",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGamesStats_Players_PlayerID",
                table: "PlayerGamesStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGamesStats_Stats_StatsID",
                table: "PlayerGamesStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Players_PlayerID",
                table: "PlayerPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Positions_PositionID",
                table: "PlayerPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_PlayerStats_PlayerStatsID",
                table: "Stats");

            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_PlayerStatsID",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_PlayerPositions_PlayerID",
                table: "PlayerPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGamesStats",
                table: "PlayerGamesStats");

            migrationBuilder.DropColumn(
                name: "PlayerStatsID",
                table: "Stats");

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "Position");

            migrationBuilder.RenameTable(
                name: "PlayerGamesStats",
                newName: "PlayerGameStats");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGamesStats_StatsID",
                table: "PlayerGameStats",
                newName: "IX_PlayerGameStats_StatsID");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGamesStats_PlayerID",
                table: "PlayerGameStats",
                newName: "IX_PlayerGameStats_PlayerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Position",
                table: "Position",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGameStats",
                table: "PlayerGameStats",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGameStats_Players_PlayerID",
                table: "PlayerGameStats",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGameStats_Stats_StatsID",
                table: "PlayerGameStats",
                column: "StatsID",
                principalTable: "Stats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Position_PositionID",
                table: "PlayerPositions",
                column: "PositionID",
                principalTable: "Position",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
