using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class AddedCoachDivision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionID",
                table: "Coaches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_DivisionID",
                table: "Coaches",
                column: "DivisionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_Divisions_DivisionID",
                table: "Coaches",
                column: "DivisionID",
                principalTable: "Divisions",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Divisions_DivisionID",
                table: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_DivisionID",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "DivisionID",
                table: "Coaches");
        }
    }
}
