using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Team_Game
    {
        public int ID { get; set; }
        [Display(Name = "Home Team")]
        [Required(ErrorMessage = "Must enter a home team.")]
        [StringLength(100, ErrorMessage = "Team name must have a max of 100 characters.")]
        public string HomeTeam {  get; set; }
        [Display(Name = "Away Team")]
        [Required(ErrorMessage = "Must enter an away team.")]
        [StringLength(100, ErrorMessage = "Team name must have a max of 100 characters.")]
        public string AwayTeam { get; set; }
        public int LineUpId { get; set; }
        public int GameID { get; set; }
    }
}
