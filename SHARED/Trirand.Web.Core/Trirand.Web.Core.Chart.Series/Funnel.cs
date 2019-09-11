namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Funnel : ChartSeries<Funnel>
	{
		public object X
		{
			get;
			set;
		}

		public object Y
		{
			get;
			set;
		}

		public object X2
		{
			get;
			set;
		}

		public object Y2
		{
			get;
			set;
		}

		public object Width
		{
			get;
			set;
		}

		public object Height
		{
			get;
			set;
		}

		public object Left
		{
			get;
			set;
		}

		public object Top
		{
			get;
			set;
		}

		public object Bottom
		{
			get;
			set;
		}

		public object Center
		{
			get;
			set;
		}

		public HorizontalType? FunnelAlign
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

		public string MinSize
		{
			get;
			set;
		}

		public string MaxSize
		{
			get;
			set;
		}

		public SortType? Sort
		{
			get;
			set;
		}

		public int? Gap
		{
			get;
			set;
		}

		public bool? LegendHoverLink
		{
			get;
			set;
		}

		public Funnel()
		{
			base.Type = ChartType.Funnel;
		}

		public Funnel(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
