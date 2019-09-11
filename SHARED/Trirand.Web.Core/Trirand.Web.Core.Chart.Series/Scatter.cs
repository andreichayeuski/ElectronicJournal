namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Scatter : Rectangular<Scatter>
	{
		public bool? Large
		{
			get;
			set;
		}

		public int? LargeThreshold
		{
			get;
			set;
		}

		public new bool? HoverAnimation
		{
			get;
			set;
		}

		public Scatter()
		{
			base.Type = ChartType.Scatter;
		}

		public Scatter(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
