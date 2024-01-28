using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Stats
    {
        public int ID { get; set; }

        public int TeamStatsID { get; set; }
        public TeamStats TeamStats { get; set; }

        public int PlayerStatsID { get; set; }
        //public PlayerStats PlayerStats { get; set; }  ---- PlayerStats is not created yet

        public int PlayerID { get; set; }
        public Player Player { get; set; }

        public int TeamID { get; set; }
        public Team Team { get; set; }

    }
}
