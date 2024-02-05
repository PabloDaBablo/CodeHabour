using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Games : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Team_GameID",
                table: "Schedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Team_GameID1",
                table: "Schedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_HomeTeam_AwayTeam",
                table: "Team_Games",
                columns: new[] { "HomeTeam", "AwayTeam" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Team_GameID1",
                table: "Schedules",
                column: "Team_GameID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Team_Games_Team_GameID1",
                table: "Schedules",
                column: "Team_GameID1",
                principalTable: "Team_Games",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Team_Games_Team_GameID1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Team_Games_HomeTeam_AwayTeam",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Team_GameID1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Team_GameID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Team_GameID1",
                table: "Schedules");
        }
    }
}
