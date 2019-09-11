using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Link
	{
		public string Name
		{
			get;
			set;
		}

		public object Source
		{
			get;
			set;
		}

		public object Target
		{
			get;
			set;
		}

		public double? Weight
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}
	}
}
