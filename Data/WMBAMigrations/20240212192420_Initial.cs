using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoachMemberID = table.Column<string>(type: "TEXT", nullable: false),
                    CoachName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CoachNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    CoachPosition = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GameTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GameLocation = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    HomeTeam = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AwayTeam = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LeagueType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Line_Ups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line_Ups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPositions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPositions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SecUser = table.Column<string>(type: "TEXT", nullable: true),
                    SecPassword = table.Column<string>(type: "TEXT", nullable: true),
                    SecRID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SecurityRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SecRRole = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DivAge = table.Column<string>(type: "TEXT", nullable: false),
                    DivisionTeams = table.Column<string>(type: "TEXT", nullable: true),
                    LeagueTypeID = table.Column<int>(type: "INTEGER", nullable: false),
                    LeagueID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Divisions_Leagues_LeagueID",
                        column: x => x.LeagueID,
                        principalTable: "Leagues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "GameLine_Up",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "INTEGER", nullable: false),
                    Team_GamesID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLine_Up", x => new { x.GamesID, x.Team_GamesID });
                    table.ForeignKey(
                        name: "FK_GameLine_Up_Games_GamesID",
                        column: x => x.GamesID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameLine_Up_Line_Ups_Team_GamesID",
                        column: x => x.Team_GamesID,
                        principalTable: "Line_Ups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Line_Up_Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    Line_UpID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line_Up_Players", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Line_Up_Players_Line_Ups_Line_UpID",
                        column: x => x.Line_UpID,
                        principalTable: "Line_Ups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerMemberID = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerFirstName = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    PlayerLastName = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    PlayerNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    DivisionID = table.Column<int>(type: "INTEGER", nullable: true),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Players_Divisions_DivisionID",
                        column: x => x.DivisionID,
                        principalTable: "Divisions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamStatsID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerStatsID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stats_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team_Coaches",
                columns: table => new
                {
                    CoachID = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Coaches", x => new { x.CoachID, x.TeamID });
                    table.ForeignKey(
                        name: "FK_Team_Coaches_Coaches_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team_Games",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayTeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeTeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Games", x => new { x.GameID, x.HomeTeamID, x.AwayTeamID });
                    table.ForeignKey(
                        name: "FK_Team_Games_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CoachID = table.Column<int>(type: "INTEGER", nullable: true),
                    DivisionID = table.Column<int>(type: "INTEGER", nullable: true),
                    Team_GameAwayTeamID = table.Column<int>(type: "INTEGER", nullable: true),
                    Team_GameAwayTeamID1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Team_GameGameID = table.Column<int>(type: "INTEGER", nullable: true),
                    Team_GameGameID1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Team_GameHomeTeamID = table.Column<int>(type: "INTEGER", nullable: true),
                    Team_GameHomeTeamID1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Teams_Coaches_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Teams_Divisions_DivisionID",
                        column: x => x.DivisionID,
                        principalTable: "Divisions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Teams_Team_Games_Team_GameGameID1_Team_GameHomeTeamID1_Team_GameAwayTeamID1",
                        columns: x => new { x.Team_GameGameID1, x.Team_GameHomeTeamID1, x.Team_GameAwayTeamID1 },
                        principalTable: "Team_Games",
                        principalColumns: new[] { "GameID", "HomeTeamID", "AwayTeamID" });
                    table.ForeignKey(
                        name: "FK_Teams_Team_Games_Team_GameGameID_Team_GameHomeTeamID_Team_GameAwayTeamID",
                        columns: x => new { x.Team_GameGameID, x.Team_GameHomeTeamID, x.Team_GameAwayTeamID },
                        principalTable: "Team_Games",
                        principalColumns: new[] { "GameID", "HomeTeamID", "AwayTeamID" });
                });

            migrationBuilder.CreateTable(
                name: "Team_Stats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamStatsPlayed = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamStatsWins = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamStatsLoss = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamStatsTies = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Stats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Team_Stats_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_CoachMemberID",
                table: "Coaches",
                column: "CoachMemberID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_LeagueID",
                table: "Divisions",
                column: "LeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_GameLine_Up_Team_GamesID",
                table: "GameLine_Up",
                column: "Team_GamesID");

            migrationBuilder.CreateIndex(
                name: "IX_Line_Up_Players_Line_UpID",
                table: "Line_Up_Players",
                column: "Line_UpID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_DivisionID",
                table: "Players",
                column: "DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerMemberID",
                table: "Players",
                column: "PlayerMemberID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerNumber_TeamID",
                table: "Players",
                columns: new[] { "PlayerNumber", "TeamID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamID",
                table: "Players",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerID",
                table: "Stats",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_TeamID",
                table: "Stats",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_TeamStatsID",
                table: "Stats",
                column: "TeamStatsID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Coaches_TeamID",
                table: "Team_Coaches",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_TeamID",
                table: "Team_Games",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Stats_TeamID",
                table: "Team_Stats",
                column: "TeamID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CoachID",
                table: "Teams",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DivisionID",
                table: "Teams",
                column: "DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Team_GameGameID_Team_GameHomeTeamID_Team_GameAwayTeamID",
                table: "Teams",
                columns: new[] { "Team_GameGameID", "Team_GameHomeTeamID", "Team_GameAwayTeamID" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Team_GameGameID1_Team_GameHomeTeamID1_Team_GameAwayTeamID1",
                table: "Teams",
                columns: new[] { "Team_GameGameID1", "Team_GameHomeTeamID1", "Team_GameAwayTeamID1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamID",
                table: "Players",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Team_Stats_TeamStatsID",
                table: "Stats",
                column: "TeamStatsID",
                principalTable: "Team_Stats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Teams_TeamID",
                table: "Stats",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Coaches_Teams_TeamID",
                table: "Team_Coaches",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Games_Teams_TeamID",
                table: "Team_Games",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisions_Leagues_LeagueID",
                table: "Divisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_Games_GameID",
                table: "Team_Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Divisions_DivisionID",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_Teams_TeamID",
                table: "Team_Games");

            migrationBuilder.DropTable(
                name: "GameLine_Up");

            migrationBuilder.DropTable(
                name: "Line_Up_Players");

            migrationBuilder.DropTable(
                name: "PlayerPositions");

            migrationBuilder.DropTable(
                name: "Security");

            migrationBuilder.DropTable(
                name: "SecurityRoles");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Team_Coaches");

            migrationBuilder.DropTable(
                name: "Line_Ups");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Team_Stats");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Team_Games");
        }
    }
}
