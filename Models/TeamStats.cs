using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class TeamStats
    {

        public int ID { get; set; }

        [Display(Name = "Games Played")]
        [Required(ErrorMessage = "Games Played cannot be empty.")]
        public int TeamStatsPlayed { get; set; }

        [Display(Name = "Win")]
        [Required(ErrorMessage= "Win cannot be empty")]
        public int TeamStatsWins { get; set; }

        [Display(Name = "Loss")]
        [Required(ErrorMessage = "Loss cannot be empty.")]
        public int TeamStatsLoss { get; set; }

        [Display(Name = "Tie")]
        [Required(ErrorMessage = "Tie cannot be empty.")]
        public int TeamStatsTies { get; set; }


        public int TeamID { get; set; }

        [Display(Name = "Team")]
        public Team Team { get; set; }

        [Display(Name = "Stats")]
        public ICollection<Stats> StatsTotal { get; set; } = new HashSet<Stats>();


    }
}
