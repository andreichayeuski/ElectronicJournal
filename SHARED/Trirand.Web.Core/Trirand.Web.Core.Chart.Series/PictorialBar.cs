namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class PictorialBar : ChartSeries<PictorialBar>
	{
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

		public string SymbolPosition
		{
			get;
			set;
		}

		public object SymbolOffset
		{
			get;
			set;
		}

		public int? SymbolRotate
		{
			get;
			set;
		}

		public object SymbolRepeat
		{
			get;
			set;
		}

		public string SymbolRepeatDirection
		{
			get;
			set;
		}

		public object SymbolMargin
		{
			get;
			set;
		}

		public bool? SymbolClip
		{
			get;
			set;
		}

		public object SymbolBoundingData
		{
			get;
			set;
		}

		public int? SymbolPatternSize
		{
			get;
			set;
		}

		public PictorialBar()
		{
			base.Type = ChartType.PictorialBar;
		}
	}
}
