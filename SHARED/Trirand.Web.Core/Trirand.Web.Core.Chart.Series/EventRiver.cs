namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class EventRiver : ChartSeries<EventRiver>
	{
		public int? XAxisIndex
		{
			get;
			set;
		}

		public int? Weight
		{
			get;
			set;
		}

		public bool? LegendHoverLink
		{
			get;
			set;
		}

		public EventRiver()
		{
			base.Type = ChartType.EventRiver;
		}

		public EventRiver(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
