namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Rectangular<T> : ChartSeries<Rectangular<T>> where T : class
	{
		public bool Smooth
		{
			get;
			set;
		}

		public string Stack
		{
			get;
			set;
		}

		public int? XAxisIndex
		{
			get;
			set;
		}

		public int? YAxisIndex
		{
			get;
			set;
		}

		public int? PolarIndex
		{
			get;
			set;
		}

		public int? GeoIndex
		{
			get;
			set;
		}

		public object Symbol
		{
			get;
			set;
		}

		public object SymbolSize
		{
			get;
			set;
		}

		public double? SymbolRotate
		{
			get;
			set;
		}

		public bool? LegendHoverLink
		{
			get;
			set;
		}

		public bool? ShowSymbol
		{
			get;
			set;
		}
	}
}
