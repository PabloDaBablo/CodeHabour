using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WMBA_7_2_.Models
{
    // Custom validation attribute for validating position names
    public class ValidPositionAttribute : ValidationAttribute
    {
        private readonly string[] _validPositions = new string[] { "P", "C", "1B", "2B", "3B", "SS", "LF", "CF", "RF" };

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
        public int PlayerPosID { get; set; }

        [ValidPosition]
        public string PlayerPosName { get; set; }
        public virtual ICollection<PlayerPosition> PlayerPositions { get; set; }
    }
}
