using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class Radar : Basic<Radar>
	{
		public ToolTip Tooltip
		{
			get;
			set;
		}

		public string Shape
		{
			get;
			set;
		}

		public int? Radius
		{
			get;
			set;
		}

		public int? StartAngle
		{
			get;
			set;
		}

		public new int? SplitNumber
		{
			get;
			set;
		}

		public IList<string> Center
		{
			get;
			set;
		}

		public new AxisName Name
		{
			get;
			set;
		}

		public IList<double> BoundaryGap
		{
			get;
			set;
		}

		public AxisLine AxisLine
		{
			get;
			set;
		}

		public AxisLabel AxisLabel
		{
			get;
			set;
		}

		public SplitLine SplitLine
		{
			get;
			set;
		}

		public SplitArea SplitArea
		{
			get;
			set;
		}

		public PolarType? Type
		{
			get;
			set;
		}

		public IList<IndicatorData> Indicator
		{
			get;
			set;
		}
	}
}
