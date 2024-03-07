using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public string ScheduleType { get; set; } //regular season or playoffs
        public int SeasonID { get; set; }
        public Season Season { get; set; }
        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
