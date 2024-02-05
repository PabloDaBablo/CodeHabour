using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Coach
    {
        [Display(Name = "Coach")]
        public int ID {  get; set; }

        [Display(Name = "Coach Member ID")]//not sure if this is required or not, but ill leave it in a comment the code
		[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Coach Member ID must contain only letters and numbers.")]
        [Required(ErrorMessage = "Coach Member ID Required.")]
		public string CoachMemberID { get; set; }

        [Display(Name = "Coach Name")]
        [Required(ErrorMessage = "Coach Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Coach name cannot be longer than 100 characters!")]
		[RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Coach Name must contain only letters")]
		public string CoachName { get; set; }

        [Display(Name = "Coach Number")]
        [Range (0, 99, ErrorMessage = "Coach number must be between 0 and 99!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Coach Number must contain only numbers.")]
        public int? CoachNumber { get; set; }

        [Display(Name = "Coach Position")]
        [Required(ErrorMessage = "Coach position cannot be blank.")]
		[RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Coach Position must contain only letters.")]
		[StringLength(75, ErrorMessage = "Coach position cannot be longer than 75 characters!")]
        public string CoachPosition { get; set; }

        [Display(Name = "Team")]
        public ICollection<Team_Coach> TeamCoach { get; set; } = new HashSet<Team_Coach>();

    }
}
