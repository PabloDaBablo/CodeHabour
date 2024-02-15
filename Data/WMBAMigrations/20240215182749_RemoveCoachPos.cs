using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class RemoveCoachPos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameLine_Up_Line_Ups_Team_GamesID",
                table: "GameLine_Up");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Team_Games_Team_GameGameID1_Team_GameTeamID1",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Team_Games_Team_GameGameID_Team_GameTeamID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Team_GameGameID_Team_GameTeamID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Team_GameGameID1_Team_GameTeamID1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Games_ID_HomeTeam_AwayTeam_GameDate_GameTime_GameLocation",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Team_GameGameID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Team_GameGameID1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Team_GameTeamID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Team_GameTeamID1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AwayTeamID",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamID",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "CoachPosition",
                table: "Coaches");

            migrationBuilder.RenameColumn(
                name: "Team_GamesID",
                table: "GameLine_Up",
                newName: "Line_UpsID");

            migrationBuilder.RenameIndex(
                name: "IX_GameLine_Up_Team_GamesID",
                table: "GameLine_Up",
                newName: "IX_GameLine_Up_Line_UpsID");

            migrationBuilder.AlterColumn<string>(
                name: "CoachMemberID",
                table: "Coaches",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ID_HomeTeam_AwayTeam_GameDate_GameTime_GameLocation",
                table: "Games",
                columns: new[] { "ID", "HomeTeam", "AwayTeam", "GameDate", "GameTime", "GameLocation" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameLine_Up_Line_Ups_Line_UpsID",
                table: "GameLine_Up",
                column: "Line_UpsID",
                principalTable: "Line_Ups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameLine_Up_Line_Ups_Line_UpsID",
                table: "GameLine_Up");

            migrationBuilder.DropIndex(
                name: "IX_Games_ID_HomeTeam_AwayTeam_GameDate_GameTime_GameLocation",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Line_UpsID",
                table: "GameLine_Up",
                newName: "Team_GamesID");

            migrationBuilder.RenameIndex(
                name: "IX_GameLine_Up_Line_UpsID",
                table: "GameLine_Up",
                newName: "IX_GameLine_Up_Team_GamesID");

            migrationBuilder.AddColumn<int>(
                name: "Team_GameGameID",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_GameGameID1",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_GameTeamID",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_GameTeamID1",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamID",
                table: "Team_Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamID",
                table: "Team_Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CoachMemberID",
                table: "Coaches",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoachPosition",
                table: "Coaches",
                type: "TEXT",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Team_GameGameID_Team_GameTeamID",
                table: "Teams",
                columns: new[] { "Team_GameGameID", "Team_GameTeamID" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Team_GameGameID1_Team_GameTeamID1",
                table: "Teams",
                columns: new[] { "Team_GameGameID1", "Team_GameTeamID1" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ID_HomeTeam_AwayTeam_GameDate_GameTime_GameLocation",
                table: "Games",
                columns: new[] { "ID", "HomeTeam", "AwayTeam", "GameDate", "GameTime", "GameLocation" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameLine_Up_Line_Ups_Team_GamesID",
                table: "GameLine_Up",
                column: "Team_GamesID",
                principalTable: "Line_Ups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Team_Games_Team_GameGameID1_Team_GameTeamID1",
                table: "Teams",
                columns: new[] { "Team_GameGameID1", "Team_GameTeamID1" },
                principalTable: "Team_Games",
                principalColumns: new[] { "GameID", "TeamID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Team_Games_Team_GameGameID_Team_GameTeamID",
                table: "Teams",
                columns: new[] { "Team_GameGameID", "Team_GameTeamID" },
                principalTable: "Team_Games",
                principalColumns: new[] { "GameID", "TeamID" });
        }
    }
}
