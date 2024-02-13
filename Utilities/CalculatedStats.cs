namespace WMBA_7_2_.Utilities
{
    public class CalculatedStats
    {
        internal static decimal CalculateAVG(int h, int ab)
        {
            decimal avg = (h / ab);
            return Math.Round(avg, 3, MidpointRounding.AwayFromZero);
        }

        internal static decimal CalculateOBP(int h, int bb, int hp, int ab, int sac) 
        {
            decimal obs = (h + bb + hp / ab + bb + hp + sac);
            return Math.Round(obs, 3, MidpointRounding.AwayFromZero);
        }

        internal static decimal CalculateSLG(int s, int d, int t, int hr, int ab)
        {
            decimal slg = (s+2*d+3*t+4*hr/ab);
            return Math.Round(slg, 3, MidpointRounding.AwayFromZero);
        }

        internal static decimal CalculateOPS(int h, int bb, int hp, int ab, int sac,
            int s, int d, int t, int hr)
        {
            decimal obs = (h + bb + hp / ab + bb + hp + sac);
            decimal slg = (s + 2 * d + 3 * t + 4 * hr / ab);
            decimal ops = obs+slg;
            return Math.Round(ops, 3, MidpointRounding.AwayFromZero);
        }
    }
}
  