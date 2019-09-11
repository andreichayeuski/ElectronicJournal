using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.bmap
{
	public class BMap
	{
		public IList<double> Center
		{
			get;
			set;
		}

		public int Zoom
		{
			get;
			set;
		}

		public bool Roam
		{
			get;
			set;
		}

		public MapStyle MapStyle
		{
			get;
			set;
		}
	}
}
