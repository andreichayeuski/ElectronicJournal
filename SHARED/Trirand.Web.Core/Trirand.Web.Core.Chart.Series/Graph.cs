namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Graph : ChartSeries<Graph>
	{
		public string Layout
		{
			get;
			set;
		}

		public object Links
		{
			get;
			set;
		}

		public object Categories
		{
			get;
			set;
		}

		public global::Trirand.Web.Core.Trirand.Web.Core.Chart.Series.graph.Force Force
		{
			get;
			set;
		}

		public Graph()
		{
			base.Type = ChartType.Graph;
		}
	}
}
