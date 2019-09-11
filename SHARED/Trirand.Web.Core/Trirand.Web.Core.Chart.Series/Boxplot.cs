using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Boxplot : Rectangular<Boxplot>
	{
		[JsonIgnore]
		public object BoxPlotData
		{
			get;
			set;
		}

		[JsonIgnore]
		public OrientType Orientation
		{
			get;
			set;
		}

		[JsonIgnore]
		public string DataClientID
		{
			get;
			set;
		}

		public Boxplot()
		{
			base.Type = ChartType.Boxplot;
		}
	}
}
