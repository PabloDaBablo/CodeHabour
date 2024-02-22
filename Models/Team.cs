using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMBA_7_2_.Models
{
    public class Team
    {
        public int ID { get; set; }

        [Display(Name = "Team Name")]
        [Required (ErrorMessage = "Team Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Team name cannot be longer than 100 characters!")]
        public string TeamName { get; set; }
        public int? CoachID { get; set; }
        [Display(Name = "Coach")]
        public Coach Coach { get; set; }

        //for now, feel free to change it if this is incorrect.
        [Display(Name = "Team Stats")]
        public TeamStats TeamStats { get; set; }

        [Display(Name = "Player")]
        public ICollection<Player> Players { get; set; } = new HashSet<Player>();

        [Display(Name = "Coach")]
        public ICollection<Team_Coach> TeamCoaches { get; set; } = new HashSet<Team_Coach>();

        [Display(Name = "Stats")]
        public ICollection<Stats> StatsTotal { get; set; } = new HashSet<Stats>();
        
        [Display(Name = "Division")]
        public int? DivisionID { get; set; }
        [Display(Name = "Division")]
        public Division Division { get; set; }
        
        public ICollection<Line_Up>LineUp { get; set; } = new HashSet<Line_Up>();
    
        public ICollection<Team_Game> Team_Games { get; set; } = new HashSet<Team_Game>();


        [Display(Name = "Home Team Games")]
        [InverseProperty("HomeTeam")]
        public ICollection<Game> HomeGames { get; set; }

        [Display(Name = "Away Team Games")]
        [InverseProperty("AwayTeam")]
        public ICollection<Game> AwayGames { get; set; }
    }
}
