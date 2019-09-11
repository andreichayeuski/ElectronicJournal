namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class TreeMap : ChartSeries<TreeMap>
	{
		public object Center
		{
			get;
			set;
		}

		public object Size
		{
			get;
			set;
		}

		public string Root
		{
			get;
			set;
		}

		public object Levels
		{
			get;
			set;
		}

		public TreeMap()
		{
			base.Type = ChartType.Treemap;
		}

		public TreeMap(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
