namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Line : Rectangular<Line>
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

		public SamplingType Sampling
		{
			get;
			set;
		}

		public Line()
		{
			base.Type = ChartType.Line;
		}

		public Line(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
