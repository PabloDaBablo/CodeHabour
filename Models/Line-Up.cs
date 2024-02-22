using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Line_Up
    {
        public int ID { get; set; }

        public int TeamID;
        public Team Team { get; set; }


        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
        public ICollection<Line_Up_Player> LineUps { get; set; } = new HashSet<Line_Up_Player>();

    }
}
