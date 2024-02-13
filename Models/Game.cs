using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
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
        public ICollection<Line_Up> Team_Games { get; set; } = new List<Line_Up>();
       // public ICollection<Line_Up> Line_Ups { get; set; } = new List<Line_Up>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Games can't be played in the past
            if (GameDate < DateTime.Now)
            {
                yield return new ValidationResult("Games must occur in the future", new[] { "GameDate" });
            }
            //Games are played during the day???
            if(GameTime.TimeOfDay < TimeSpan.FromHours(9) || GameTime.TimeOfDay >= TimeSpan.FromHours(20))
            {
                yield return new ValidationResult("Games must be scheduled after 9 am and before 8 pm.", new[] { "GameTime" });
            }
            //Games are played at an approved field
            //if (GameLocation != "Chippawa Park Diamond" || GameLocation != "Maple Park Diamond #1" || GameLocation != "Maple Park Diamond #2"
            //    || GameLocation != "Memorial Park Diamond #2" || GameLocation != "Burger Park Diamond" || GameLocation != "Memorial Park Diamond #3"
            //    || GameLocation != "Welland Jackfish Stadium")
            //{
            //    yield return new ValidationResult("Games must be scheduled at an appropriate field.", new[] { "GameLocation" });
            //}
            //Home team and Visiting team can't be the same team
            if (HomeTeam == AwayTeam)
            {
                yield return new ValidationResult("Home and Visitor must be different teams", new[] { "HomeTeam","AwayTeam" });
            }
        }
    }
}
