using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class IndicatorData
	{
		public string Name
		{
			get;
			set;
		}

		public string Text
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

		public AxisLabel AxisLabel
		{
			get;
			set;
		}

		public IndicatorData()
		{
		}

		public IndicatorData(string text)
		{
			Text = text;
		}
	}
}
