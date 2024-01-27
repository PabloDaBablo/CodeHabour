using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Divisions
    {

        public int DivID { get; set; }

        [Display(Name = "Division Age")]
        [Required(ErrorMessage = "Division Age cannot be empty.")]
        public int DivAge { get; set; }

        [Display(Name = "Division Team")]
        [Required(ErrorMessage = "Division Team cannot be empty.")]
        public string DivisionTeams { get; set; }

        [Required(ErrorMessage = "You must select a League Type")]
        [Display(Name = "League Type")]
        public int LeagueTypeID { get; set; }

        [Display(Name = "League")]
        public League League { get; set; }




    }
}
