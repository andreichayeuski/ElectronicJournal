namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class CategoryAxis : ChartAxis<CategoryAxis>
	{
		public bool? BoundaryGap
		{
			get;
			set;
		}

		public CategoryAxis()
		{
			base.Type = AxisType.Category;
		}
	}
}
