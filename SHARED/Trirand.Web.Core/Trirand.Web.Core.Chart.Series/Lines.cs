namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Lines : Rectangular<Line>
	{
		public bool? ShowAllSymbol
		{
			get;
			set;
		}

		public new bool? Smooth
		{
			get;
			set;
		}

		public object DataFilter
		{
			get;
			set;
		}

		public object Step
		{
			get;
			set;
		}

		public bool? Large
		{
			get;
			set;
		}

		public double? LargeThreshold
		{
			get;
			set;
		}

		public SamplingType Sampling
		{
			get;
			set;
		}

		public Lines()
		{
			base.Type = ChartType.Line;
		}

		public Lines(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
