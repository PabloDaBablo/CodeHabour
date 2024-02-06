using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class addedDivisionToTeamNUllable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Divisions_DivisionID",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionID",
                table: "Teams",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Divisions_DivisionID",
                table: "Teams",
                column: "DivisionID",
                principalTable: "Divisions",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Divisions_DivisionID",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionID",
                table: "Teams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Divisions_DivisionID",
                table: "Teams",
                column: "DivisionID",
                principalTable: "Divisions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
