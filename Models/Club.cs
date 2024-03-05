namespace WMBA_7_2_.Models
{
    public class Club
    {
        public int ID { get; set; }
        public string ClubName { get; set; }

        public ICollection<Division> Divisions = new HashSet<Division>();
    }
}
