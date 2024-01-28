using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Line_Up_Players",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "CoachID",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerID",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CoachID",
                table: "Teams",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PlayerID",
                table: "Teams",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Coaches_CoachID",
                table: "Teams",
                column: "CoachID",
                principalTable: "Coaches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_PlayerID",
                table: "Teams",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Coaches_CoachID",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_PlayerID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CoachID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_PlayerID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CoachID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Line_Up_Players",
                newName: "Id");
        }
    }
}
