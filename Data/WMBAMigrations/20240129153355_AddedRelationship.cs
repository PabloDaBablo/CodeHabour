using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class AddedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_PlayerID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_PlayerID",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "TeamCoachID",
                table: "Team_Coaches",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ScheduleID",
                table: "Schedules",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "DivID",
                table: "Divisions",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "PlayerID1",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamID",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PlayerID1",
                table: "Teams",
                column: "PlayerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamID",
                table: "Players",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Players_Teams_TeamID",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Players_PlayerID1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_PlayerID1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PlayerID1",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Team_Coaches",
                newName: "TeamCoachID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Schedules",
                newName: "ScheduleID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Divisions",
                newName: "DivID");

            migrationBuilder.AlterColumn<int>(
                name: "TeamID",
                table: "Players",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PlayerID",
                table: "Teams",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamID",
                table: "Players",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Players_PlayerID",
                table: "Teams",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
