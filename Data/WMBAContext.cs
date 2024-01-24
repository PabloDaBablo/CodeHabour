using Microsoft.EntityFrameworkCore;

namespace WMBA_7_2_.Data
{
    public class WMBAContext : DbContext
    {
        public WMBAContext(DbContextOptions<WMBAContext> options) : base(options) { }


        //public DbSet<classname> WMBA { get; set; }
        //to add models to the database I believe
    }
}
