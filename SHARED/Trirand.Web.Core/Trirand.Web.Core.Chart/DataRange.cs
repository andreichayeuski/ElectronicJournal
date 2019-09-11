using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartFeature;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class DataRange : Basic<DataRange>
	{
		public new OrientType? Orient
		{
			get;
			set;
		}

		public new int? ItemGap
		{
			get;
			set;
		}

		public int? ItemHeight
		{
			get;
			set;
		}

		public new int? Min
		{
			get;
			set;
		}

		public new int? Max
		{
			get;
			set;
		}

		public int? Precision
		{
			get;
			set;
		}

		public new int? SplitNumber
		{
			get;
			set;
		}

		public IList<Split> SplitList
		{
			get;
			set;
		}

		public Range Range
		{
			get;
			set;
		}

		public object SelectedMode
		{
			get;
			set;
		}

		public bool? Calculable
		{
			get;
			set;
		}

		public bool? HoverLink
		{
			get;
			set;
		}

		public bool? Realtime
		{
			get;
			set;
		}

		public IList<string> Color
		{
			get;
			set;
		}

		public object Formatter
		{
			get;
			set;
		}

		public IList<string> Text
		{
			get;
			set;
		}

		public TextStyle TextStyle
		{
			get;
			set;
		}
	}
}
