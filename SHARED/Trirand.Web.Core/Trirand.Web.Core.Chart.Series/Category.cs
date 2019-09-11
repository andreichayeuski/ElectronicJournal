using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Category
	{
		public string Name
		{
			get;
			set;
		}

		public string Symbol
		{
			get;
			set;
		}

		public object SymbolSize
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public Category(string name)
		{
			Name = name;
		}
	}
}
