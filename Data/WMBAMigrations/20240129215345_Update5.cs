using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_layerID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_layerID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "layerID",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "layerID",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_layerID",
                table: "Teams",
                column: "layerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_layerID",
                table: "Teams",
                column: "layerID",
                principalTable: "Players",
                principalColumn: "ID");
        }
    }
}
