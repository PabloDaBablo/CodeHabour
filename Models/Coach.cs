using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Coach
    {

        public int ID {  get; set; }

        [Display(Name = "Coach Member ID")]//not sure if this is required or not, but ill leave it in a comment the code
        //[Required(ErrorMessage = "Coach Member ID Required.)] also not sure if its a string or int lol
        public int CoachMemberID { get; set; }

        [Display(Name = "Coach Name")]
        [Required(ErrorMessage = "Coach Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Coach name cannot be longer than 100 characters!")]
        public string CoachName { get; set; }

        [Display(Name = "Coach Number")]
        
        public int? CoachNumber { get; set; }

        [Display(Name = "Coach Position")]
        [Required(ErrorMessage = "Coach position cannot be blank.")]
        [StringLength(75, ErrorMessage = "Coach position cannot be longer than 75 characters!")]
        public string CoachPosition { get; set; }

        [Display(Name = "Coach")]
        public ICollection<Team_Coach> TeamCoach { get; set; } = new HashSet<Team_Coach>();


    }
}
