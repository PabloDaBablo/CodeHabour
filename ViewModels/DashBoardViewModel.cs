using Microsoft.AspNetCore.Mvc.Rendering;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.ViewModels
{
    public class DashboardViewModel
    {
        // Properties from the Game model
        public int ID { get; set; }
        public DateTime GameDate { get; set; }
        public DateTime GameTime { get; set; }
        public string GameLocation { get; set; }
        public int HomeTeamID { get; set; }
        public Team HomeTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamID { get; set; }
        public Team AwayTeam { get; set; }
        public int AwayTeamScore { get; set; }
        public Ballparks Ballparks { get; set; }
        public ICollection<Team_Game> Team_Games { get; set; }
        public ICollection<Line_Up> Line_Ups { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
        public ICollection<PlayerGameStats> PlayerGameStats { get; set; }

		public DashboardViewModel(Game game)
        {
            if (game != null)
            {
                ID = game.ID;
                GameDate = game.GameDate;
                GameTime = game.GameTime;
                GameLocation = game.GameLocation;
                HomeTeamID = game.HomeTeamID;
                HomeTeam = game.HomeTeam;
                HomeTeamScore = game.HomeTeamScore;
                AwayTeamID = game.AwayTeamID;
                AwayTeam = game.AwayTeam;
                AwayTeamScore = game.AwayTeamScore;
                Ballparks = game.Ballparks;
                Team_Games = game.Team_Games;
                Line_Ups = game.Line_Ups;
                GamePlayers = game.GamePlayers;
                PlayerGameStats = game.PlayerGameStats;
            }
        }

        // Constructor
		public DashboardViewModel()
		{

		}
    }
}
