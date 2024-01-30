using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_PlayerID1",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "PlayerID1",
                table: "Teams",
                newName: "layerID");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_PlayerID1",
                table: "Teams",
                newName: "IX_Teams_layerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_layerID",
                table: "Teams",
                column: "layerID",
                principalTable: "Players",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_layerID",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "layerID",
                table: "Teams",
                newName: "PlayerID1");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_layerID",
                table: "Teams",
                newName: "IX_Teams_PlayerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_PlayerID1",
                table: "Teams",
                column: "PlayerID1",
                principalTable: "Players",
                principalColumn: "ID");
        }
    }
}
