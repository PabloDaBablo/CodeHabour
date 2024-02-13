using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
    public class Team_Game
    {
        public int ID { get; set; }
        //[Display(Name = "Home")]
        //[Required(ErrorMessage = "Must enter a home team.")]
        //[StringLength(100, ErrorMessage = "Team name must have a max of 100 characters.")]
        //public string HomeTeam {  get; set; }
        //[Display(Name = "Visitor")]
        //[Required(ErrorMessage = "Must enter an away team.")]
        //[StringLength(100, ErrorMessage = "Team name must have a max of 100 characters.")]
        //public string AwayTeam { get; set; }
        public int TeamID { get; set; }
        public Team Team { get; set; }
        public int GameID { get; set; }
        public Game Game { get; set; }
        public int AwayTeamID { get; set; }
        public ICollection<Team> AwayTeams { get; set; } = new List<Team>();
        public int HomeTeamID {  get; set; }
        public ICollection<Team>HomeTeams { get; set; } = new List<Team>();
        
    }
}
