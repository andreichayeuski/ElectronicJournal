namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Bar : Rectangular<Bar>
	{
		public object BarGap
		{
			get;
			set;
		}

		public object BarCategoryGap
		{
			get;
			set;
		}

		public int? BarMinHeight
		{
			get;
			set;
		}

		public object BarWidth
		{
			get;
			set;
		}

		public int? BarHeight
		{
			get;
			set;
		}

		public int? BarMaxWidth
		{
			get;
			set;
		}

		public Bar()
		{
			base.Type = ChartType.Bar;
		}

		public Bar(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
