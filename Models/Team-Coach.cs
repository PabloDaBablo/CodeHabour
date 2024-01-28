using System.Diagnostics.Contracts;

namespace WMBA_7_2_.Models
{
    public class Team_Coach
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int CoachId { get; set; }
    }
}
