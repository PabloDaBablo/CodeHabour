using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WMBA_7_2_.Models
{
    // Custom validation attribute for validating position names
    public class ValidPositionAttribute : ValidationAttribute
    {
        private readonly string[] _validPositions = new string[] { "P", "C", "1B", "2B", "3B", "SS", "LF", "CF", "RF", "LCF", "RCF" };
       //i think this is ok. i added two positions LCF and RCF for the U9 to replace the P and CF in that age group;
       //they have 4 outfielders and no pitcher ~donaven
        public ValidPositionAttribute()
        {
            ErrorMessage = "Enter a valid position name";
        }

        public override bool IsValid(object value)
        {
            if (value is string positionName)
            {
                return _validPositions.Contains(positionName);
            }
            return false;
        }
    }

    public class Position
    {
        public int ID { get; set; }

        [ValidPosition]
        public string PlayerPosName { get; set; }
        public ICollection<PlayerPosition> PlayerPositions { get; set; } = new HashSet<PlayerPosition>();
    }
}
