using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Team
    {

        public int ID { get; set; }

        [Display(Name = "Team Name")]
        [Required (ErrorMessage = "Team Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Team name cannot be longer than 100 characters!")]
        public string TeamName { get; set; }

        //for now, feel free to change it if this is incorrect.
        [Display(Name = "Team Stats")]
        public TeamStats TeamStats { get; set; }

        [Display(Name = "Player")]
        public ICollection<Player> Players { get; set; } = new HashSet<Player>();

        [Display(Name = "Coach")]
        public ICollection<Team_Coach> TeamCoaches { get; set; } = new HashSet<Team_Coach>();


    }
}
