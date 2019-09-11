using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class Legend : Basic<Legend>, IData<Legend>
	{
		public new OrientType? Orient
		{
			get;
			set;
		}

		public HorizontalType? Align
		{
			get;
			set;
		}

		public new object ItemGap
		{
			get;
			set;
		}

		public int? ItemWidth
		{
			get;
			set;
		}

		public int? ItemHeight
		{
			get;
			set;
		}

		public TextStyle TextStyle
		{
			get;
			set;
		}

		public object Formatter
		{
			get;
			set;
		}

		public object SelectedMode
		{
			get;
			set;
		}

		public Dictionary<string, bool> Selected
		{
			get;
			set;
		}

		public object Data
		{
			get;
			set;
		}

		public object Type
		{
			get;
			set;
		}
	}
}
