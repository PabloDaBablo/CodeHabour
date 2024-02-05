using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Team_Coach
    {

        public int ID { get; set; }
        public int CoachID { get; set; }
        public Coach Coach { get; set;}
        public int TeamID { get; set; }
        public Team Team { get; set; }
        
    }
}
