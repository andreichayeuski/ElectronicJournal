namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class AngleAxis : ChartAxis<AngleAxis>
	{
		public int? PolarIndex
		{
			get;
			set;
		}

		public int? StartAngle
		{
			get;
			set;
		}

		public bool? Clockwise
		{
			get;
			set;
		}

		public bool? BoundaryGap
		{
			get;
			set;
		}
	}
}
