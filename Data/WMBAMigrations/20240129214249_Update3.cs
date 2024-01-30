using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerID1",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PlayerID1",
                table: "Teams",
                column: "PlayerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_PlayerID1",
                table: "Teams",
                column: "PlayerID1",
                principalTable: "Players",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_PlayerID1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_PlayerID1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerID1",
                table: "Teams");
        }
    }
}
