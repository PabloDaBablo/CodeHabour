using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class PlayerPosition
    {
        public int ID { get; set; }

        [Display(Name = "Player")]
        public int PlayerID { get; set; }

        [Display(Name = "Player")]
        public Player Player { get; set; }

        [Display(Name = "Position")]
        public int PositionID { get; set; }

        [Display(Name = "Position")]
        public Position Position { get; set; }
    }
}
