using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Gauge : ChartSeries<Gauge>
	{
		public IList<string> Center
		{
			get;
			set;
		}

		public object Radius
		{
			get;
			set;
		}

		public int? StartAngle
		{
			get;
			set;
		}

		public int? EndAngle
		{
			get;
			set;
		}

		public int? Min
		{
			get;
			set;
		}

		public int? Max
		{
			get;
			set;
		}

		public int? SplitNumber
		{
			get;
			set;
		}

		public AxisLine AxisLine
		{
			get;
			set;
		}

		public AxisTick AxisTick
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

		public GaugePointer Pointer
		{
			get;
			set;
		}

		public GaugeTitle Title
		{
			get;
			set;
		}

		public GaugeDetail Detail
		{
			get;
			set;
		}

		public bool? LegendHoverLink
		{
			get;
			set;
		}

		public int? Precision
		{
			get;
			set;
		}

		public Gauge()
		{
			base.Type = ChartType.Gauge;
		}
	}
}
