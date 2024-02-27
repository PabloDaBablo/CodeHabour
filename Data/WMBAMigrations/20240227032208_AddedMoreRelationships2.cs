using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class AddedMoreRelationships2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions");

            migrationBuilder.DropIndex(
                name: "IX_PlayerPositions_PlayerID",
                table: "PlayerPositions");

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "Stats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "PlayerPositions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "PlayerGamesStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions",
                columns: new[] { "PlayerID", "PositionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_GameID",
                table: "Stats",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGamesStats_GameID",
                table: "PlayerGamesStats",
                column: "GameID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGamesStats_Games_GameID",
                table: "PlayerGamesStats",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Games_GameID",
                table: "Stats",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGamesStats_Games_GameID",
                table: "PlayerGamesStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Games_GameID",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_GameID",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions");

            migrationBuilder.DropIndex(
                name: "IX_PlayerGamesStats_GameID",
                table: "PlayerGamesStats");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "PlayerGamesStats");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "PlayerPositions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerPositions",
                table: "PlayerPositions",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositions_PlayerID",
                table: "PlayerPositions",
                column: "PlayerID");
        }
    }
}
