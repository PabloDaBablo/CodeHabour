namespace WMBA_7_2_.Utilities
{
	public class CalculatedStats
	{
		internal static decimal CalculateAVG(int? hits, int? atBats)
		{
			if (atBats.HasValue && atBats.Value > 0)
			{
				decimal avg = ((decimal)hits.Value / atBats.Value);
				return Math.Round(avg, 3, MidpointRounding.AwayFromZero);
			}
			else
			{
				return 0;
			}
		}

		internal static decimal CalculateOBP(int? hits, int? bb, int? hp, int? ab, int? sac)
		{
			if (ab.HasValue && ab.Value > 0)
			{
				decimal obs = (((decimal)hits.Value + bb.Value + hp.Value) / (ab.Value + bb.Value + hp.Value + sac.Value));
				return Math.Round(obs, 3, MidpointRounding.AwayFromZero);
			}
			else
			{
				return 0;
			}
		}

		internal static decimal CalculateSLG(int? s, int? d, int? t, int? hr, int? ab)
		{
			if (ab.HasValue && ab.Value > 0)
			{
				decimal slg = ((decimal)(s.Value + 2 * d.Value + 3 * t.Value + 4 * hr.Value) / ab.Value);
				return Math.Round(slg, 3, MidpointRounding.AwayFromZero);
			}
			else
			{
				return 0;
			}
		}

		internal static decimal CalculateOPS(int? h, int? bb, int? hp, int? ab, int? sac, int? s, int? d, int? t, int? hr)
		{
			if (ab.HasValue && ab.Value > 0)
			{
				decimal obs = ((decimal)(h.Value + bb.Value + hp.Value) / (ab.Value + bb.Value + hp.Value + sac.Value));
				decimal slg = ((decimal)(s.Value + 2 * d.Value + 3 * t.Value + 4 * hr.Value) / ab.Value);
				decimal ops = obs + slg;
				return Math.Round(ops, 3, MidpointRounding.AwayFromZero);
			}
			else
			{
				return 0;
			}
		}
	}
}