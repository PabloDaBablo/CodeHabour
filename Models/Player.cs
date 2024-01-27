using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Player
    {

        public int PlayerID { get; set; }


        [Display(Name = "Player Member ID ")]
        public int PlayerMemberID { get; set; }

        [Display(Name = "Player Name")]
        [Required(ErrorMessage = "Player name cannot be empty!")]
        [StringLength(75, ErrorMessage = "Player name cannot be longer than 75 characters!")]
        public string PlayerName { get; set; }

        [Display(Name = "Player Number")]
        [Required(ErrorMessage = "Player number cannot be empty!")]
        public int PlayerNumber { get; set; }

        // didnt have time to finish this model class, have to go to bed 


    }
}
