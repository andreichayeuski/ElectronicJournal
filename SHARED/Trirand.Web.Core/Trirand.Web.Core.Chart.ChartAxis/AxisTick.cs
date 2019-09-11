using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class AxisTick
	{
		public bool? Show
		{
			get;
			set;
		}

		public object Interval
		{
			get;
			set;
		}

		public int SplitNumber
		{
			get;
			set;
		}

		public bool? OnGap
		{
			get;
			set;
		}

		public bool? Inside
		{
			get;
			set;
		}

		public int? Length
		{
			get;
			set;
		}

		public LineStyle LineStyle
		{
			get;
			set;
		}

		public bool? AlignWithLabel
		{
			get;
			set;
		}
	}
}
