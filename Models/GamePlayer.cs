namespace WMBA_7_2_.Models
{
    public class GamePlayer
    {

        public int ID { get; set; }

        public TeamLineup TeamLineup { get; set; }

        public int GameID { get; set; }
        public Game Game { get; set; }

        public int PlayerID { get; set; }
        public Player Player { get; set; }



    }
}
