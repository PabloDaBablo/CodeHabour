using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{
	public class PlayerStats
	{
		public int ID { get; set; }

		[Display(Name = "Plate Appearances")]//issues but done
		public int? PA { get; set; }

		[Display(Name = "Games Played")]//issues but done
		public int? GP { get; set; }

		[Display(Name = "Runs")]
		public int? Runs { get; set; }//done

		[Display(Name = "Hits")]
		public int? Hits { get; set; }

		[Display(Name = "Singles")]
		public int? B1 { get; set; }//done

		[Display(Name = "Doubles")]
		public int? B2 { get; set; }//done

		[Display(Name = "Triples")]
		public int? B3 { get; set; }//done

		[Display(Name = "Home Runs")]
		public int? HR { get; set; }//done

		[Display(Name = "Runs Batted In")]
		public int? RBI { get; set; }// not done, conditional, so that if you hit someone else in and they score a run, they get counted as a run, and you get an RBI

		[Display(Name = "Base on Balls")]
		public int? BB { get; set; }// not done, when the pitcher throws four balls, the player is advanced to first base/walks

		[Display(Name = "Strikeouts")]
		public int? K { get; set; }//done

		[Display(Name = "Stolen Bases")]	
		public int? SB { get; set; }//not done

		[Display(Name = "Sacrifice")]
		public int? SAC { get; set; } // not done

		[Display(Name = "Ground Outs")]
		public int? GO { get; set; }//done 

		[Display(Name = "Pop Outs")]
		public int? PO { get; set; }//done

		[Display(Name = "Fly Outs")]
		public int? FO { get; set; }//done

		[Display(Name = "Player")]
		public int PlayerID { get; set; }

		[Display(Name = "Player")]
		public Player Player { get; set; }

		public ICollection<Stats> Stats { get; set; } = new HashSet<Stats>();
	}
}
