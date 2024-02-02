﻿using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Player
    {
        public int ID { get; set; }

        [Display(Name = "Player Member ID ")]
        public int PlayerMemberID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Player name cannot be empty!")]
        [StringLength(75, ErrorMessage = "Player name cannot be longer than 75 characters!")]
        public string PlayerFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Player name cannot be empty!")]
        [StringLength(75, ErrorMessage = "Player name cannot be longer than 75 characters!")]
        public string PlayerLastName { get; set; }
        [Display(Name = "Player Number")]
        [Required(ErrorMessage = "Player number cannot be empty!")]
        public int PlayerNumber { get; set; }

        [Display(Name = "Stats")]
        public ICollection<Stats> StatsTotal { get; set; } = new HashSet<Stats>();

        [Display(Name = "Division")]
        public int? DivisionID { get; set; }

        [Display(Name = "Division")]
        public Division Division { get; set; }

        [Display(Name = "Team")]
        public int? TeamID { get; set; }

        [Display(Name = "Team")]
        public Team Team { get; set; }

        [Display(Name = "Full Name")]
        public string PlayerFullName => $"{PlayerFirstName} {PlayerLastName}";
    }
}
