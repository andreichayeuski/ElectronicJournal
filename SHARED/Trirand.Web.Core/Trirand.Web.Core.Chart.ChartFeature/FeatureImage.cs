using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartFeature
{
	public class FeatureImage
	{
		public bool? Show
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public IList<string> Lang
		{
			get;
			set;
		}

		public int? PixelRatio
		{
			get;
			set;
		}
	}
}
