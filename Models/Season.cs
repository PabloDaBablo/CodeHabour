namespace WMBA_7_2_.Models
{
    public class Season
    {
        public int ID { get; set; }
        public string Year { get; set; }

        public ICollection<Schedule> Schedules = new HashSet<Schedule>();
    }
}
