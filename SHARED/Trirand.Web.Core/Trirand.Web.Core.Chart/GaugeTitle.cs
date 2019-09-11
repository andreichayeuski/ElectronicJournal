using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class GaugeTitle
	{
		public bool? Show
		{
			get;
			set;
		}

		public IList<string> OffsetCenter
		{
			get;
			set;
		}

		public TextStyle TextStyle
		{
			get;
			set;
		}
	}
}
