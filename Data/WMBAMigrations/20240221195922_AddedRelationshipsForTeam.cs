using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class AddedRelationshipsForTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Line_Ups_LineUpID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LineUpID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LineUpID",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "TeamID1",
                table: "Line_Ups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Line_Ups_TeamID1",
                table: "Line_Ups",
                column: "TeamID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Ups_Teams_TeamID1",
                table: "Line_Ups",
                column: "TeamID1",
                principalTable: "Teams",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Line_Ups_Teams_TeamID1",
                table: "Line_Ups");

            migrationBuilder.DropIndex(
                name: "IX_Line_Ups_TeamID1",
                table: "Line_Ups");

            migrationBuilder.DropColumn(
                name: "TeamID1",
                table: "Line_Ups");

            migrationBuilder.AddColumn<int>(
                name: "LineUpID",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LineUpID",
                table: "Teams",
                column: "LineUpID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Line_Ups_LineUpID",
                table: "Teams",
                column: "LineUpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");
        }
    }
}
