namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Parallel : ChartSeries<Parallel>
	{
		public int? ParalleIndex
		{
			get;
			set;
		}

		public bool? Realtime
		{
			get;
			set;
		}

		public Parallel()
		{
			base.Type = ChartType.Parallel;
		}

		public Parallel(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
