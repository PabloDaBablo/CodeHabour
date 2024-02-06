using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class addedDivisionToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Line_UpID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameSeason",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Line_UpID",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "DivisionID",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameLine_Up",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "INTEGER", nullable: false),
                    Line_UpsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLine_Up", x => new { x.GamesID, x.Line_UpsID });
                    table.ForeignKey(
                        name: "FK_GameLine_Up_Games_GamesID",
                        column: x => x.GamesID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameLine_Up_Line_Ups_Line_UpsID",
                        column: x => x.Line_UpsID,
                        principalTable: "Line_Ups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DivisionID",
                table: "Teams",
                column: "DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerMemberID",
                table: "Players",
                column: "PlayerMemberID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameLine_Up_Line_UpsID",
                table: "GameLine_Up",
                column: "Line_UpsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Divisions_DivisionID",
                table: "Teams",
                column: "DivisionID",
                principalTable: "Divisions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Divisions_DivisionID",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "GameLine_Up");

            migrationBuilder.DropIndex(
                name: "IX_Teams_DivisionID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerMemberID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "DivisionID",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "GameSeason",
                table: "Games",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Line_UpID",
                table: "Games",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Line_UpID",
                table: "Games",
                column: "Line_UpID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Line_Ups_Line_UpID",
                table: "Games",
                column: "Line_UpID",
                principalTable: "Line_Ups",
                principalColumn: "ID");
        }
    }
}
