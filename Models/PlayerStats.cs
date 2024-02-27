using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
	public class PlayerStats
	{
		public int ID { get; set; }

		[Display(Name = "Plate Appearances")]
		public int PA { get; set; }

		[Display(Name = "Runs")]
		public int Runs { get; set; }

		[Display(Name = "Hits")]
		public int Hits { get; set; }

		[Display(Name = "1st Base")]
		public int B1 { get; set; }

		[Display(Name = "2nd Base")]
		public int B2 { get; set; }

		[Display(Name = "3rd Base")]
		public int B3 { get; set; }

		[Display(Name = "Home Runs")]
		public int HR { get; set; }

		[Display(Name = "Runs Batted In")]
		public int RBI { get; set; }

		[Display(Name = "Base on Balls")]
		public int BB { get; set; }

		[Display(Name = "Strikeouts")]
		public int K { get; set; }

		[Display(Name = "Stolen Bases")]	
		public int SB { get; set; }

		[Display(Name = "Sacrifice")]
		public int SAC { get; set; }

		[Display(Name = "Player")]
		public int PlayerID { get; set; }

		[Display(Name = "Player")]
		public Player Player { get; set; }

		public ICollection<Stats> Stats { get; set; } = new HashSet<Stats>();
	}
}
