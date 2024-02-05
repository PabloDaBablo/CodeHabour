using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Games3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Line_Ups_Line_UpID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Team_Games_Team_GameID1",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_Schedules_GameID",
                table: "Team_Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_Team_GameID1",
                table: "Games",
                newName: "IX_Games_Team_GameID1");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_Line_UpID",
                table: "Games",
                newName: "IX_Games_Line_UpID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Team_Games_Team_GameID1",
                table: "Games",
                column: "Team_GameID1",
                principalTable: "Team_Games",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Games_Games_GameID",
                table: "Team_Games",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Team_Games_Team_GameID1",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_Games_GameID",
                table: "Team_Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Team_GameID1",
                table: "Schedules",
                newName: "IX_Schedules_Team_GameID1");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Line_UpID",
                table: "Schedules",
                newName: "IX_Schedules_Line_UpID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Line_Ups_Line_UpID",
                table: "Schedules",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Team_Games_Team_GameID1",
                table: "Schedules",
                column: "Team_GameID1",
                principalTable: "Team_Games",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Games_Schedules_GameID",
                table: "Team_Games",
                column: "GameID",
                principalTable: "Schedules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
