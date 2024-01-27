using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class TeamStats
    {

        public int TeamStatsID { get; set; }

        [Display(Name = "Games Played")]
        [Required(ErrorMessage = "Games Played cannot be empty.")]
        public int TeamStatsPlayed { get; set; }

        [Display(Name = "Wins")]
        [Required(ErrorMessage= "Wins cannot be empty")]
        public int TeamStatsWins { get; set; }

        [Display(Name = "Losses")]
        [Required(ErrorMessage = "Losses cannot be empty.")]
        public int TeamStatsLoss { get; set; }

        [Display(Name = "Ties")]
        [Required(ErrorMessage = "Ties cannot be empty.")]
        public int TeamStatsTies { get; set; }


        public int TeamID { get; set; }

        [Display(Name = "Team")]
        public Team Team { get; set; }




    }
}
