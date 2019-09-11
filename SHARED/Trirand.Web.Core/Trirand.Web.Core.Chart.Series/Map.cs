using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Map : ChartSeries<Map>
	{
		public SelectedModeType? SelectedMode
		{
			get;
			set;
		}

		public string MapType
		{
			get;
			set;
		}

		public bool? Hoverable
		{
			get;
			set;
		}

		public bool? DataRangeHoverLink
		{
			get;
			set;
		}

		public object MapLocation
		{
			get;
			set;
		}

		public MapValCalType? MapValueCalculation
		{
			get;
			set;
		}

		public int? MapValuePrecision
		{
			get;
			set;
		}

		public bool? ShowLegendSymbol
		{
			get;
			set;
		}

		public object Roam
		{
			get;
			set;
		}

		public object ScaleLimit
		{
			get;
			set;
		}

		public object NameMap
		{
			get;
			set;
		}

		public object TextFixed
		{
			get;
			set;
		}

		public object GeoCoord
		{
			get;
			set;
		}

		public object Heatmap
		{
			get;
			set;
		}

		[JsonProperty("map")]
		public string MapName
		{
			get;
			set;
		}

		public Map()
		{
			base.Type = ChartType.Map;
		}

		public Map(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
