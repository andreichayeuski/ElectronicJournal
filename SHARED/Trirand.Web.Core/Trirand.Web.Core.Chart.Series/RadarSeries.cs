using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class RadarSeries : ChartSeries<Radar>
	{
		public IList<string> Center
		{
			get;
			set;
		}

		public int? RadarIndex
		{
			get;
			set;
		}

		public string Symbol
		{
			get;
			set;
		}

		public object SymbolSize
		{
			get;
			set;
		}

		public int? SymbolRotate
		{
			get;
			set;
		}

		public bool? LegendHoverLink
		{
			get;
			set;
		}

		public IList<IndicatorData> Indicator
		{
			get;
			set;
		}

		public RadarSeries()
		{
			base.Type = ChartType.Radar;
		}

		public RadarSeries(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
