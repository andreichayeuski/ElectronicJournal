using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartFeature
{
	public class DataView
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

		public bool? ReadOnly
		{
			get;
			set;
		}

		public IList<string> Lang
		{
			get;
			set;
		}
	}
}
