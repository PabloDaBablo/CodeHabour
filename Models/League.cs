using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class League
    {

        public int ID { get; set; }

        [Display(Name = "League Type")]
        [Required(ErrorMessage = "League Type cannot be empty.")]
        public string LeagueType { get; set; }

        public ICollection<Division> Divisions { get; set; } = new HashSet<Division>();

    }
}
