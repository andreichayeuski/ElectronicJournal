using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonMultipleSearch
	{
		public string groupOp
		{
			get;
			set;
		}

		public List<MultipleSearchRule> rules
		{
			get;
			set;
		}
	}
}
