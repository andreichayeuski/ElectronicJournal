using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class Parallel : Basic<Parallel>
	{
		public bool? AxisExpandable
		{
			get;
			set;
		}

		public int? AxisExpandCenter
		{
			get;
			set;
		}

		public int? AxisExpandCount
		{
			get;
			set;
		}

		public int? AxisExpandWidth
		{
			get;
			set;
		}

		public Axis ParallelAxisDefault
		{
			get;
			set;
		}
	}
}
