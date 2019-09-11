namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class ChartSeries<T> : Series, IData<T> where T : class
	{
		public object Data
		{
			get;
			set;
		}
	}
}
