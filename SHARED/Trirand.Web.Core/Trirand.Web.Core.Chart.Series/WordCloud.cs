namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class WordCloud : ChartSeries<WordCloud>
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

		public object TextPadding
		{
			get;
			set;
		}

		public object TextRotation
		{
			get;
			set;
		}

		public AutoSizeConfig AutoSize
		{
			get;
			set;
		}

		public WordCloud()
		{
			base.Type = ChartType.WordCloud;
		}

		public WordCloud(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
