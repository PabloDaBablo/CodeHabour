using System.ComponentModel.DataAnnotations;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.ViewModels
{
    public class TeamVM
    {
        public int ID { get; set; }

        [Display(Name = "Team Name")]
        [Required(ErrorMessage = "Team Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Team name cannot be longer than 100 characters!")]
        public string TeamName { get; set; }

        public int CoachID { get; set; }
        [Display(Name = "Coach")]
        public Coach Coach { get; set; }

        public IEnumerable<int> SelectedPlayerID { get; set; }
    }
}
