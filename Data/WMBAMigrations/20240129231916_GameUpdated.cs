using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class GameUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Line_UpID",
                table: "Games",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");
        }
    }
}
