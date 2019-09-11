using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class AxisPointer : Basic<AxisPointer>
	{
		public new bool? Show
		{
			get;
			set;
		}

		public AxisPointType? Type
		{
			get;
			set;
		}

		public bool? Snap
		{
			get;
			set;
		}

		public AxisLink Link
		{
			get;
			set;
		}

		public AxisLabel Label
		{
			get;
			set;
		}

		public LineStyle LineStyle
		{
			get;
			set;
		}

		public LineStyle CrossStyle
		{
			get;
			set;
		}

		public AreaStyle ShadowStyle
		{
			get;
			set;
		}
	}
}
