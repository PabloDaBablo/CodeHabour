using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class GamePlayerClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Line_Ups_Players_PlayerID1",
                table: "Line_Ups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team_Games",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Team_Games_TeamID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Line_Ups_PlayerID1",
                table: "Line_Ups");

            migrationBuilder.DropIndex(
                name: "IX_Games_ID_HomeTeam_AwayTeam_GameDate_GameTime_GameLocation",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayerID1",
                table: "Line_Ups");

            migrationBuilder.DropColumn(
                name: "AwayTeam",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeam",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "IsHomeTeam",
                table: "Team_Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team_Games",
                table: "Team_Games",
                columns: new[] { "TeamID", "GameID" });

            migrationBuilder.CreateTable(
                name: "GamePlayers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamLineup = table.Column<int>(type: "INTEGER", nullable: false),
                    GameID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GamePlayers_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayers_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_GameID",
                table: "Team_Games",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamID",
                table: "Games",
                column: "AwayTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamID",
                table: "Games",
                column: "HomeTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ID_HomeTeamID_AwayTeamID_GameDate_GameTime_GameLocation",
                table: "Games",
                columns: new[] { "ID", "HomeTeamID", "AwayTeamID", "GameDate", "GameTime", "GameLocation" });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_GameID",
                table: "GamePlayers",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_PlayerID_GameID",
                table: "GamePlayers",
                columns: new[] { "PlayerID", "GameID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_AwayTeamID",
                table: "Games",
                column: "AwayTeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamID",
                table: "Games",
                column: "HomeTeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_AwayTeamID",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamID",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GamePlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team_Games",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Team_Games_GameID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_AwayTeamID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_HomeTeamID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ID_HomeTeamID_AwayTeamID_GameDate_GameTime_GameLocation",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsHomeTeam",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "AwayTeamID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamID",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "PlayerID1",
                table: "Line_Ups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwayTeam",
                table: "Games",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeam",
                table: "Games",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team_Games",
                table: "Team_Games",
                columns: new[] { "GameID", "TeamID" });

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_TeamID",
                table: "Team_Games",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Line_Ups_PlayerID1",
                table: "Line_Ups",
                column: "PlayerID1");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ID_HomeTeam_AwayTeam_GameDate_GameTime_GameLocation",
                table: "Games",
                columns: new[] { "ID", "HomeTeam", "AwayTeam", "GameDate", "GameTime", "GameLocation" });

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Ups_Players_PlayerID1",
                table: "Line_Ups",
                column: "PlayerID1",
                principalTable: "Players",
                principalColumn: "ID");
        }
    }
}
