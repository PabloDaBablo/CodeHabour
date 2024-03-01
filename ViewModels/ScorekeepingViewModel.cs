using WMBA_7_2_.Models;
using System.Collections.Generic;

namespace WMBA_7_2_.ViewModels
{
    public class ScoreKeepingViewModel
    {
        public Game SelectedGame { get; set; }

        // Home and Away Teams
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        // Players
        public List<Player> HomeTeamPlayers { get; set; }
        public List<Player> AwayTeamPlayers { get; set; }
        // Teams
        public List<Team> AllTeams { get; set; }
    }
}
