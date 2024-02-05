using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Game3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Team_Games_Team_GameID1",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_Line_Ups_Line_UpID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Team_Games_Line_UpID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Team_GameID1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "AwayTeam",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "HomeTeam",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "Line_UpID",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "Team_GameID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Team_GameID1",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "LineUpID",
                table: "Team_Games",
                newName: "TeamID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_TeamID",
                table: "Team_Games",
                column: "TeamID");

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
                name: "FK_Team_Games_Teams_TeamID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Team_Games_TeamID",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "AwayTeam",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeam",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TeamID",
                table: "Team_Games",
                newName: "LineUpID");

            migrationBuilder.AddColumn<string>(
                name: "AwayTeam",
                table: "Team_Games",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeam",
                table: "Team_Games",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Line_UpID",
                table: "Team_Games",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team_GameID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team_GameID1",
                table: "Games",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_Line_UpID",
                table: "Team_Games",
                column: "Line_UpID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Team_GameID1",
                table: "Games",
                column: "Team_GameID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Team_Games_Team_GameID1",
                table: "Games",
                column: "Team_GameID1",
                principalTable: "Team_Games",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Games_Line_Ups_Line_UpID",
                table: "Team_Games",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");
        }
    }
}
