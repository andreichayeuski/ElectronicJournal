using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class TimeAxis : ChartAxis<TimeAxis>
	{
		public IList<double> BoundaryGap
		{
			get;
			set;
		}

		public TimeAxis()
		{
			base.Type = AxisType.Time;
		}
	}
}
