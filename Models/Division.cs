using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Division
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Division")]
        [Required(ErrorMessage = "Division Age cannot be empty.")]
        public string DivAge { get; set; }

        [Required(ErrorMessage = "You must select a League Type")]
        [Display(Name = "League Type")]
        public int LeagueTypeID { get; set; }

        [Display(Name = "League")]
        public League League { get; set; }

        [Display (Name = "Team")]
		public ICollection<Team> Teams { get; set; } = new HashSet<Team>();

	}
}
