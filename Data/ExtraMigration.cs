using Microsoft.EntityFrameworkCore.Migrations;

namespace WMBA_7_2_.Data
{
    public class ExtraMigration
    {
        public static void Steps(MigrationBuilder migrationBuilder)
        {
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
