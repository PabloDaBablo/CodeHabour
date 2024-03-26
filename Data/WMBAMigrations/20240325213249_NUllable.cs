using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class NUllable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGamesStats_Players_PlayerID",
                table: "PlayerGamesStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGamesStats_Stats_StatsID",
                table: "PlayerGamesStats");

            migrationBuilder.AddColumn<int>(
                name: "HBP",
                table: "PlayerStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatsID",
                table: "PlayerGamesStats",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "SLG",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "PlayerGamesStats",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "OPS",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "OBP",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "AVG",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_DivAge",
                table: "Divisions",
                column: "DivAge",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGamesStats_Players_PlayerID",
                table: "PlayerGamesStats",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGamesStats_Stats_StatsID",
                table: "PlayerGamesStats",
                column: "StatsID",
                principalTable: "Stats",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGamesStats_Players_PlayerID",
                table: "PlayerGamesStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGamesStats_Stats_StatsID",
                table: "PlayerGamesStats");

            migrationBuilder.DropIndex(
                name: "IX_Divisions_DivAge",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "HBP",
                table: "PlayerStats");

            migrationBuilder.AlterColumn<int>(
                name: "StatsID",
                table: "PlayerGamesStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SLG",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "PlayerGamesStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OPS",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OBP",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AVG",
                table: "PlayerGamesStats",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGamesStats_Players_PlayerID",
                table: "PlayerGamesStats",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGamesStats_Stats_StatsID",
                table: "PlayerGamesStats",
                column: "StatsID",
                principalTable: "Stats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
