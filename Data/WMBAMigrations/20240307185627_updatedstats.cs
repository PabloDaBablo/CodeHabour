using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMBA_7_2_.Data.WMBAMigrations
{
    /// <inheritdoc />
    public partial class updatedstats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Schedules_ScheduleID",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Games_ScheduleID",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Season",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "ScheduleID",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Season",
                newName: "Seasons");

            migrationBuilder.AddColumn<int>(
                name: "FO",
                table: "PlayerStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GO",
                table: "PlayerStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PO",
                table: "PlayerStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "FO",
                table: "PlayerStats");

            migrationBuilder.DropColumn(
                name: "GO",
                table: "PlayerStats");

            migrationBuilder.DropColumn(
                name: "PO",
                table: "PlayerStats");

            migrationBuilder.RenameTable(
                name: "Seasons",
                newName: "Season");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleID",
                table: "Games",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Season",
                table: "Season",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeasonID = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedules_Season_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Season",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ScheduleID",
                table: "Games",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SeasonID",
                table: "Schedules",
                column: "SeasonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Schedules_ScheduleID",
                table: "Games",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ID");
        }
    }
}
