using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class RoamController : Basic<RoamController>
	{
		public new int? Width
		{
			get;
			set;
		}

		public new int? Height
		{
			get;
			set;
		}

		public string FillerColor
		{
			get;
			set;
		}

		public string HandleColor
		{
			get;
			set;
		}

		public int? Step
		{
			get;
			set;
		}

		public Dictionary<string, bool> MapTypeControl
		{
			get;
			set;
		}
	}
}
