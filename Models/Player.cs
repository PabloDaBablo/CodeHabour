﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WMBA_7_2_.Models
{

    public class Player
    {
        //for the purposes of the excel i had to make the
        //playernumber nullable and add the division stuff ~donaven
        public int ID { get; set; }

        [Display(Name = "Player Member ID ")]
        public string PlayerMemberID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Player name cannot be empty!")]
        [StringLength(75, ErrorMessage = "Player name cannot be longer than 75 characters!")]
        public string PlayerFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Player name cannot be empty!")]
        [StringLength(75, ErrorMessage = "Player name cannot be longer than 75 characters!")]
        public string PlayerLastName { get; set; }

        [Display(Name = "Player Number")]
        [Range(0, 99, ErrorMessage = "Player number must be between 0 and 99!")]
        public int? PlayerNumber { get; set; }


        [Display(Name = "Division")]
        public int? DivisionID { get; set; }

        [Display(Name = "Division")]
        public Division Division { get; set; }

        [Display(Name = "Team")]
        public int? TeamID { get; set; }

        [Display(Name = "Team")]
        public Team Team { get; set; }

        [Display(Name = "Full Name")]
        public string PlayerFullName => $"{PlayerFirstName} {PlayerLastName}";

        [Display(Name = "Status")]
        public bool IsActive { get; set; } = true;

        public ICollection<Line_Up_Player> LineUps { get; set; } = new HashSet<Line_Up_Player>();

        [Display(Name = "Game Players")]
        public ICollection<GamePlayer> GamePlayers { get; set; } = new HashSet<GamePlayer>();

        [Display(Name = "Player Game Stats")]
        public ICollection<PlayerGameStats> PlayerGameStats { get; set; } = new HashSet<PlayerGameStats>();

        [Display(Name = "Player Stats")]
        public ICollection<Stats> Stats { get; set; } = new HashSet<Stats>();
    
        public ICollection<PlayerPosition> PlayerPositions { get; set; } = new HashSet<PlayerPosition>();

        public ICollection<PlayerStats>PlayerStats { get; set; } = new HashSet<PlayerStats>();
    }
}
