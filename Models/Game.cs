using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int ScheduleID {  get; set; }
        public ICollection<Schedule> Schedules { get; set; } = new HashSet<Schedule>();
    }
}
