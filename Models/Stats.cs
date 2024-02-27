using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Stats
    {
        public int ID { get; set; }

        public int TeamStatsID { get; set; }
        public TeamStats TeamStats { get; set; }


        public int PlayerID { get; set; }
        public Player Player { get; set; }

        public int TeamID { get; set; }
        public Team Team { get; set; }

        public ICollection<PlayerGameStats> PlayerGameStats { get; set; } = new HashSet<PlayerGameStats>();                                                                                                          

        public int GameID { get; set; }
        public Game Game { get; set; }

    }
}
