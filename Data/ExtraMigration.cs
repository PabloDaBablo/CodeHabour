using Microsoft.EntityFrameworkCore.Migrations;

namespace WMBA_7_2_.Data
{
    public class ExtraMigration
    {
        public static void Steps(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @"
                    CREATE TRIGGER SetDivisionTimestampOnUpdate
                    AFTER UPDATE ON Divisions
                    BEGIN
                        UPDATE Divisions
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetDivisionTimestampOnInsert
                    AFTER INSERT ON Divisions
                    BEGIN
                        UPDATE Divisions
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
               @"
                    CREATE TRIGGER SetTeamTimestampOnUpdate
                    AFTER UPDATE ON Teams
                    BEGIN
                        UPDATE Teams
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetTeamTimestampOnInsert
                    AFTER INSERT ON Teams
                    BEGIN
                        UPDATE Teams
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
               @"
                    CREATE TRIGGER SetPlayerTimestampOnUpdate
                    AFTER UPDATE ON Players
                    BEGIN
                        UPDATE Players
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetPlayerTimestampOnInsert
                    AFTER INSERT ON Players
                    BEGIN
                        UPDATE Players
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            //migrationBuilder.Sql(
            //    @"
            //        Drop View IF EXISTS [Reports];
            //        Create View Reports AS
            //        Select r.FirstName, r.LastName, r.MemberID, r.Division, r.Team
            //        From ImportReport ir 
            //    ");
        }
    }
}
