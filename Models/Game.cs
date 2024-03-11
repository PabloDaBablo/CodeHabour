using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{

    public enum TeamLineup
    {
        Home,
        Away
    }
    public class Game : IValidatableObject
    {
        public int ID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date cannot be empty!")]
        public DateTime GameDate { get; set; }

        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true , DataFormatString = "{0:HH:mm}")]
        [Required(ErrorMessage = "Time cannot be empty!")]
        public DateTime GameTime { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location cannot be empty!")]
        [StringLength (75, ErrorMessage = "Location cannot be longer than 75 characters!")]
        public string GameLocation { get; set; }

        [Display(Name = "Home Team")]
        [Required(ErrorMessage = "You must select the Home Team for the Game.")]
        public int HomeTeamID { get; set; }

        [Display(Name = "Home Team")]
        public Team HomeTeam { get; set; }

        [Display(Name = "Home Team Score")]
        [Range(0, int.MaxValue, ErrorMessage = "Score must be a non-negative number.")]
        public int HomeTeamScore { get; set; }

        [Display(Name = "Away Team")]
        [Required(ErrorMessage = "You must select the Away Team for the Game.")]
        public int AwayTeamID { get; set; }

        [Display(Name = "Away Team")]
        public Team AwayTeam { get; set; }

        [Display(Name = "Away Team Score")]
        [Range(0, int.MaxValue, ErrorMessage = "Score must be a non-negative number.")]
        public int AwayTeamScore { get; set; }

        public ICollection<Team_Game> Team_Games { get; set; } = new List<Team_Game>();
        public ICollection<Line_Up> Line_Ups { get; set; } = new List<Line_Up>();

        [Display(Name = "Game Players")]
        public ICollection<GamePlayer> GamePlayers { get; set; } = new HashSet<GamePlayer>();

        [Display(Name = "Player Game Stats")]
        public ICollection<PlayerGameStats> PlayerGameStats { get; set; } = new HashSet<PlayerGameStats>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Games can't be played in the past
            DateTime checkDateTime = GameDate.Date.Add(GameTime.TimeOfDay);
            if (checkDateTime <= DateTime.Now.AddSeconds(1))
            {
                yield return new ValidationResult("Game must occur in the future.", new[] { "GameDate", "GameTime" });
            }
            //Games are played during the day
            if (GameTime.TimeOfDay < TimeSpan.FromHours(9) || GameTime.TimeOfDay >= TimeSpan.FromHours(20))
            {
                yield return new ValidationResult("Games must be scheduled after 9 am and before 8 pm.", new[] { "GameTime" });
            }
       
            //Home team and Visiting team can't be the same team
            if (HomeTeamID == AwayTeamID)
            {
                yield return new ValidationResult("Home and Visitor must be different teams", new[] { "HomeTeam","AwayTeam" });
            }
        }
    }
}
