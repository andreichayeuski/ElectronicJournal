namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Tree : ChartSeries<Tree>
	{
		public LocationData RootLocation
		{
			get;
			set;
		}

		public int? LayerPadding
		{
			get;
			set;
		}

		public int? NodePadding
		{
			get;
			set;
		}

		public OrientType? Orient
		{
			get;
			set;
		}

		public DirectionType? Direction
		{
			get;
			set;
		}

		public object Roam
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

		public Tree()
		{
			base.Type = ChartType.Tree;
		}

		public Tree(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
