namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Sankey : ChartSeries<Sankey>
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

		public Sankey()
		{
			base.Type = ChartType.Sankey;
		}

		public Sankey(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
