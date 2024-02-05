using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Game
    {
        public int ID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date cannot be empty!")]
        public DateTime GameDate { get; set; }

        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Time cannot be empty!")]
        public DateTime GameTime { get; set; }

        //I dont know if we need this or not ~donaven
        //[Display(Name = "Season")]
        //[Required(ErrorMessage = "Season cannot be empty!")]
        //[StringLength (50, ErrorMessage = "Season cannot be longer than 50 characters")]
        //public string GameSeason { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location cannot be empty!")]
        [StringLength (75, ErrorMessage = "Location cannot be longer than 75 characters!")]
       
        public string GameLocation { get; set; }
        [Display(Name = "Home")]
        [Required(ErrorMessage = "Must enter a home team.")]
        [StringLength(100, ErrorMessage = "Team name must have a max of 100 characters.")]
        public string HomeTeam { get; set; }
        [Display(Name = "Visitor")]
        [Required(ErrorMessage = "Must enter an away team.")]
        [StringLength(100, ErrorMessage = "Team name must have a max of 100 characters.")]
        public string AwayTeam { get; set; }
        public ICollection<Team_Game> Team_Games { get; set; } = new HashSet<Team_Game>();
        public ICollection<Line_Up> Line_Ups { get; set; } = new HashSet<Line_Up>();
    }
}
