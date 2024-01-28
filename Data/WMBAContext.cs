using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.Data
{
    public class WMBAContext : DbContext
    {
        public WMBAContext(DbContextOptions<WMBAContext> options) : base(options) { }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Divisions> Divisions { get; set; }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Line_Up>Line_Ups { get; set; }
        public DbSet<Line_Up_Player> Line_Up_Players { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerPosition> PlayerPositions { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Security> Security { get; set; }
        public DbSet<SecurityRole>SecurityRoles { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Team> Teams { get; set; }  
        public DbSet<Team_Coach> Team_Coaches { get; set; }
        public DbSet<Team_Game> Team_Games { get; set; }
        public DbSet<TeamStats> Team_Stats { get; set; }
       





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Team_Coach>()
                .HasKey(tc => new { tc.CoachID, tc.TeamID});

        }

    }
}
