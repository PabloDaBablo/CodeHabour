using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class PlayerGameStats
    {

        public int ID { get; set; }

        [Display(Name = "Average")]
        public decimal? AVG { get; set; }

        [Display(Name = "On Base Percentage")]
        public decimal? OBP { get; set; }

        [Display(Name = "Slugging Percentage")]
        public decimal? SLG { get; set; }

        [Display(Name = "On Base Plus Slugging")]
        public decimal? OPS { get; set; }


        public int? StatsID { get; set; }
        public Stats Stats { get; set; }

        public int? PlayerID { get; set; }
        public Player Player { get; set; }
    }
}
