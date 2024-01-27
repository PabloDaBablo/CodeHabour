using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.Data
{
    public class WMBAContext : DbContext
    {
        public WMBAContext(DbContextOptions<WMBAContext> options) : base(options) { }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Divisions> Divisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Team_Coach>()
                .HasKey(tc => new { tc.CoachID, tc.TeamID});

        }

    }
}
