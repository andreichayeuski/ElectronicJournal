using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class HeatMap : ChartSeries<HeatMap>
	{
		public int? BlurSize
		{
			get;
			set;
		}

		public bool? Hoverable
		{
			get;
			set;
		}

		public IList<GradientColorData> GradientColors
		{
			get;
			set;
		}

		public double? MinAlpha
		{
			get;
			set;
		}

		public int? ValueScale
		{
			get;
			set;
		}

		public double? Opacity
		{
			get;
			set;
		}

		public new string CoordinateSystem
		{
			get;
			set;
		}

		public HeatMap()
		{
			base.Type = ChartType.Heatmap;
		}

		public HeatMap(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
