using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class TreeData
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

		public int? Value
		{
			get;
			set;
		}

		public IList<TreeData> Children
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
