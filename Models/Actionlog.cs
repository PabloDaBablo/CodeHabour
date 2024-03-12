namespace WMBA_7_2_.Models
{
    public class ActionLog
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public string Data { get; set; } 
        public DateTime Timestamp { get; set; }

        public int? GameID { get; set; }
    }
}
