using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WMBA_7_2_.Models;
using WMBA_7_2_.ViewModels;

namespace WMBA_7_2_.Data
{
    public class WMBAContext : DbContext
    {
        public WMBAContext(DbContextOptions<WMBAContext> options) : base(options) { }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Line_Up>Line_Ups { get; set; }
        public DbSet<Line_Up_Player> Line_Up_Players { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerPosition> PlayerPositions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Security> Security { get; set; }
        public DbSet<SecurityRole>SecurityRoles { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Team> Teams { get; set; }  
        public DbSet<Team_Coach> Team_Coaches { get; set; }
        public DbSet<Team_Game> Team_Games { get; set; }
        public DbSet<TeamStats> Team_Stats { get; set; }
        public DbSet<ImportReport> Reports { get; set; }
        public DbSet<Rules> Rules { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerGameStats> PlayerGamesStats { get; set; }

        public DbSet<PlayerStats> PlayerStats { get; set; }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<ActionLog> ActionLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Team_Coach>()
                .HasKey(tc => new { tc.CoachID, tc.TeamID});


			modelBuilder.Entity<Team_Coach>()
		        .HasOne(tc => tc.Team)
		        .WithMany(t => t.TeamCoaches)
		        .HasForeignKey(tc => tc.TeamID);

			modelBuilder.Entity<Team_Coach>()
				.HasOne(tc => tc.Coach)
				.WithMany(c => c.TeamCoach)
				.HasForeignKey(tc => tc.CoachID);

            modelBuilder.Entity<PlayerPosition>()
                .HasKey(pp => new { pp.PlayerID, pp.PositionID });

            modelBuilder.Entity<PlayerPosition>()
                .HasOne(pp => pp.Player)
                .WithMany(p => p.PlayerPositions)
                .HasForeignKey(pp => pp.PlayerID);

            modelBuilder.Entity<PlayerPosition>()
                .HasOne(pp => pp.Position)
                .WithMany(p => p.PlayerPositions)
                .HasForeignKey(pp => pp.PositionID);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany( t => t.Players)
                .HasForeignKey( p => p.TeamID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasIndex(p => p.PlayerMemberID).IsUnique(); //changed this from player number to player member id since playerNumber can be the same, but playerMemberID cannot.

            modelBuilder.Entity<Coach>()
                .HasIndex(p => p.CoachMemberID).IsUnique();

			modelBuilder.Entity<Player>()
	            .HasIndex(p => new { p.PlayerNumber, p.TeamID }) //this should work instead, yes?
	            .IsUnique();

            modelBuilder.Entity<Game>()
                .HasIndex(tg => new { tg.ID, tg.HomeTeamID, tg.AwayTeamID, tg.GameDate, tg.GameTime, tg.GameLocation });

            modelBuilder.Entity<ImportReport>()
                .ToView(nameof(Reports))
                .HasKey(r => r.ID);


            modelBuilder.Entity<Team_Game>()
                .HasKey(tg => new { tg.TeamID, tg.GameID });

            modelBuilder.Entity<Team_Game>()
                .HasOne(tg => tg.Team)
                .WithMany(t => t.Team_Games)
                .HasForeignKey(tg => tg.TeamID);

            modelBuilder.Entity<Team_Game>()
                .HasOne(tg => tg.Game)
                .WithMany(g => g.Team_Games)
                .HasForeignKey(tg => tg.GameID);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamID)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GamePlayer>()
              .HasIndex(c => new { c.PlayerID, c.GameID})
              .IsUnique();

            modelBuilder.Entity<PlayerStats>()
            .HasOne(ps => ps.Player)
            .WithMany(p => p.PlayerStats) 
            .HasForeignKey(ps => ps.PlayerID);
        }
    }
}
