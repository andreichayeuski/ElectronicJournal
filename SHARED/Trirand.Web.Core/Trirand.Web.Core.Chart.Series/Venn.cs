namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Venn : ChartSeries<Venn>
	{
		public Venn()
		{
			base.Type = ChartType.Venn;
		}

		public Venn(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
