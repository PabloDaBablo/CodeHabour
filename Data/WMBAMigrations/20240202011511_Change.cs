using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_TeamID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Line_UpID",
                table: "Team_Games",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionID",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Line_UpID",
                table: "Games",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "DivAge",
                table: "Divisions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_GameID",
                table: "Team_Games",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Games_Line_UpID",
                table: "Team_Games",
                column: "Line_UpID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_DivisionID",
                table: "Players",
                column: "DivisionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Divisions_DivisionID",
                table: "Players",
                column: "DivisionID",
                principalTable: "Divisions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Games_Games_GameID",
                table: "Team_Games",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Games_Line_Ups_Line_UpID",
                table: "Team_Games",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Divisions_DivisionID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_Games_GameID",
                table: "Team_Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_Line_Ups_Line_UpID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Team_Games_GameID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Team_Games_Line_UpID",
                table: "Team_Games");

            migrationBuilder.DropIndex(
                name: "IX_Players_DivisionID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Line_UpID",
                table: "Team_Games");

            migrationBuilder.DropColumn(
                name: "DivisionID",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "Line_UpID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamID",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DivAge",
                table: "Divisions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamID",
                table: "Games",
                column: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamID",
                table: "Games",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
