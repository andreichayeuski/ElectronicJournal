namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class K : Rectangular<K>
	{
		public int? BarWidth
		{
			get;
			set;
		}

		public int? BarMaxWidth
		{
			get;
			set;
		}

		public K()
		{
			base.Type = ChartType.K;
		}

		public K(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
