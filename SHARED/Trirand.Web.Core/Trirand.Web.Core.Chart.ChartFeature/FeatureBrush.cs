using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartFeature
{
	public class FeatureBrush
	{
		public IList<BrushType> Type
		{
			get;
			set;
		}

		public BrushIcon Icon
		{
			get;
			set;
		}

		public BrushTitle Title
		{
			get;
			set;
		}
	}
}
