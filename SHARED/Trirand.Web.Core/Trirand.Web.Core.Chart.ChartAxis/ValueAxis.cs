namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class ValueAxis : ChartAxis<ValueAxis>
	{
		public object BoundaryGap
		{
			get;
			set;
		}

		public ValueAxis()
		{
			base.Type = AxisType.Value;
		}
	}
}
