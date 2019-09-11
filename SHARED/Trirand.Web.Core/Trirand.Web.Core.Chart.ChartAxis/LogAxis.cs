namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class LogAxis : ChartAxis<LogAxis>
	{
		public bool? LogPositive
		{
			get;
			set;
		}

		public int? LogLabelBase
		{
			get;
			set;
		}

		public LogAxis()
		{
			base.Type = AxisType.Log;
		}
	}
}
