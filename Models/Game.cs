using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Game
    {
        public int ID { get; set; }
        public int ScheduleID {  get; set; }
        [Display(Name = "Line-Up")]
        public Line_Up Line_Up { get; set; }
        [Display(Name = "Line-Up")]
        public int Line_UpID {  get; set; }
        [Display(Name = "Team")]
        public int TeamID { get; set; }

        [Display(Name = "Team")]
        public Team Team { get; set; }
        ICollection<Line_Up> Line_Ups { get; set; } = new HashSet<Line_Up>();

    }
}
