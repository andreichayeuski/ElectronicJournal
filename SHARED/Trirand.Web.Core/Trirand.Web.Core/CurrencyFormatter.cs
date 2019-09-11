namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CurrencyFormatter : CoreGridColumnFormatter
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

		public string Prefix
		{
			get;
			set;
		}

		public string Suffix
		{
			get;
			set;
		}
	}
}
