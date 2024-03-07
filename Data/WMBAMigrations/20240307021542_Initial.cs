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
                name: "Clubs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClubName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.ID);
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
                name: "Positions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerPosName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RuleName = table.Column<string>(type: "TEXT", nullable: true),
                    RuleDescription = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.ID);
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
                name: "Schedules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleType = table.Column<string>(type: "TEXT", nullable: true),
                    SeasonID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedules_Season_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Season",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoachMemberID = table.Column<string>(type: "TEXT", nullable: true),
                    CoachName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CoachNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    DivisionID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Coaches_Divisions_DivisionID",
                        column: x => x.DivisionID,
                        principalTable: "Divisions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DivisionID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Teams_Divisions_DivisionID",
                        column: x => x.DivisionID,
                        principalTable: "Divisions",
                        principalColumn: "ID");
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
                    HomeTeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayTeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Games_Teams_AwayTeamID",
                        column: x => x.AwayTeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Teams_HomeTeamID",
                        column: x => x.HomeTeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Line_Ups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamID1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line_Ups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Line_Ups_Teams_TeamID1",
                        column: x => x.TeamID1,
                        principalTable: "Teams",
                        principalColumn: "ID");
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
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team_Coaches",
                columns: table => new
                {
                    CoachID = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    ID = table.Column<int>(type: "INTEGER", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Team_Coaches_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Team_Games",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    GameID = table.Column<int>(type: "INTEGER", nullable: false),
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    IsHomeTeam = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Games", x => new { x.TeamID, x.GameID });
                    table.ForeignKey(
                        name: "FK_Team_Games_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_Games_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameLine_Up",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "INTEGER", nullable: false),
                    Line_UpsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLine_Up", x => new { x.GamesID, x.Line_UpsID });
                    table.ForeignKey(
                        name: "FK_GameLine_Up_Games_GamesID",
                        column: x => x.GamesID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameLine_Up_Line_Ups_Line_UpsID",
                        column: x => x.Line_UpsID,
                        principalTable: "Line_Ups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    table.ForeignKey(
                        name: "FK_Line_Up_Players_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPositions",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionID = table.Column<int>(type: "INTEGER", nullable: false),
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPositions", x => new { x.PlayerID, x.PositionID });
                    table.ForeignKey(
                        name: "FK_PlayerPositions_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPositions_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PA = table.Column<int>(type: "INTEGER", nullable: true),
                    Runs = table.Column<int>(type: "INTEGER", nullable: true),
                    Hits = table.Column<int>(type: "INTEGER", nullable: true),
                    B1 = table.Column<int>(type: "INTEGER", nullable: true),
                    B2 = table.Column<int>(type: "INTEGER", nullable: true),
                    B3 = table.Column<int>(type: "INTEGER", nullable: true),
                    HR = table.Column<int>(type: "INTEGER", nullable: true),
                    RBI = table.Column<int>(type: "INTEGER", nullable: true),
                    BB = table.Column<int>(type: "INTEGER", nullable: true),
                    K = table.Column<int>(type: "INTEGER", nullable: true),
                    SB = table.Column<int>(type: "INTEGER", nullable: true),
                    SAC = table.Column<int>(type: "INTEGER", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamStatsID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<int>(type: "INTEGER", nullable: false),
                    GameID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerStatsID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stats_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stats_PlayerStats_PlayerStatsID",
                        column: x => x.PlayerStatsID,
                        principalTable: "PlayerStats",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stats_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stats_Team_Stats_TeamStatsID",
                        column: x => x.TeamStatsID,
                        principalTable: "Team_Stats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stats_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerGamesStats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AVG = table.Column<decimal>(type: "TEXT", nullable: false),
                    OBP = table.Column<decimal>(type: "TEXT", nullable: false),
                    SLG = table.Column<decimal>(type: "TEXT", nullable: false),
                    OPS = table.Column<decimal>(type: "TEXT", nullable: false),
                    StatsID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    GameID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGamesStats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerGamesStats_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PlayerGamesStats_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerGamesStats_Stats_StatsID",
                        column: x => x.StatsID,
                        principalTable: "Stats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_CoachMemberID",
                table: "Coaches",
                column: "CoachMemberID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_DivisionID",
                table: "Coaches",
                column: "DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_LeagueID",
                table: "Divisions",
                column: "LeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_GameLine_Up_Line_UpsID",
                table: "GameLine_Up",
                column: "Line_UpsID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_GameID",
                table: "GamePlayers",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_PlayerID_GameID",
                table: "GamePlayers",
                columns: new[] { "PlayerID", "GameID" },
                unique: true);

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
                name: "IX_Games_ScheduleID",
                table: "Games",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Line_Up_Players_Line_UpID",
                table: "Line_Up_Players",
                column: "Line_UpID");

            migrationBuilder.CreateIndex(
                name: "IX_Line_Up_Players_PlayerID",
                table: "Line_Up_Players",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Line_Ups_TeamID1",
                table: "Line_Ups",
                column: "TeamID1");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGamesStats_GameID",
                table: "PlayerGamesStats",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGamesStats_PlayerID",
                table: "PlayerGamesStats",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGamesStats_StatsID",
                table: "PlayerGamesStats",
                column: "StatsID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositions_PositionID",
                table: "PlayerPositions",
                column: "PositionID");

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
                name: "IX_PlayerStats_PlayerID",
                table: "PlayerStats",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SeasonID",
                table: "Schedules",
                column: "SeasonID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_GameID",
                table: "Stats",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerID",
                table: "Stats",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlayerStatsID",
                table: "Stats",
                column: "PlayerStatsID");

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
                name: "IX_Team_Games_GameID",
                table: "Team_Games",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Stats_TeamID",
                table: "Team_Stats",
                column: "TeamID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DivisionID",
                table: "Teams",
                column: "DivisionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "GameLine_Up");

            migrationBuilder.DropTable(
                name: "GamePlayers");

            migrationBuilder.DropTable(
                name: "Line_Up_Players");

            migrationBuilder.DropTable(
                name: "PlayerGamesStats");

            migrationBuilder.DropTable(
                name: "PlayerPositions");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Security");

            migrationBuilder.DropTable(
                name: "SecurityRoles");

            migrationBuilder.DropTable(
                name: "Team_Coaches");

            migrationBuilder.DropTable(
                name: "Team_Games");

            migrationBuilder.DropTable(
                name: "Line_Ups");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "Team_Stats");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
