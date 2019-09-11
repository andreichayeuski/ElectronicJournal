namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class NumberFormatter : CoreGridColumnFormatter
	{
		public string ThousandsSeparator
		{
			get;
			set;
		}

		public string DefaultValue
		{
			get;
			set;
		}

		public string DecimalSeparator
		{
			get;
			set;
		}

		public int DecimalPlaces
		{
			get;
			set;
		}
	}
}
