using System.ComponentModel.DataAnnotations;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.ViewModels
{
    public class TeamVM
    {
		
			public int ID { get; set; }
			public string TeamName { get; set; }
			public int DivisionID { get; set; }
			public List<int> Players { get; set; } 
			public List<int> Coaches { get; set; } 
	}
}
