using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartFeature
{
	public class MagicType
	{
		public bool? Show
		{
			get;
			set;
		}

		public MagicTitle Title
		{
			get;
			set;
		}

		public MagicOption Option
		{
			get;
			set;
		}

		public IList<object> Type
		{
			get;
			set;
		}
	}
}
