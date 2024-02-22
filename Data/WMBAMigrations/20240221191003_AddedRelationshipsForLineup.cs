using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class AddedRelationshipsForLineup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LineUpID",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerID1",
                table: "Line_Ups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LineUpID",
                table: "Teams",
                column: "LineUpID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Line_Ups_PlayerID1",
                table: "Line_Ups",
                column: "PlayerID1");

            migrationBuilder.CreateIndex(
                name: "IX_Line_Up_Players_PlayerID",
                table: "Line_Up_Players",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Up_Players_Players_PlayerID",
                table: "Line_Up_Players",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Ups_Players_PlayerID1",
                table: "Line_Ups",
                column: "PlayerID1",
                principalTable: "Players",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Line_Ups_LineUpID",
                table: "Teams",
                column: "LineUpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Line_Up_Players_Players_PlayerID",
                table: "Line_Up_Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Line_Ups_Players_PlayerID1",
                table: "Line_Ups");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Line_Ups_LineUpID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LineUpID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Line_Ups_PlayerID1",
                table: "Line_Ups");

            migrationBuilder.DropIndex(
                name: "IX_Line_Up_Players_PlayerID",
                table: "Line_Up_Players");

            migrationBuilder.DropColumn(
                name: "LineUpID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerID1",
                table: "Line_Ups");
        }
    }
}
