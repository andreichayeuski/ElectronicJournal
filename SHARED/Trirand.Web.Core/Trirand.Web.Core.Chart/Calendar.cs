using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class Calendar : Basic<Calendar>
	{
		public object CellSize
		{
			get;
			set;
		}

		public object Range
		{
			get;
			set;
		}

		public SplitLine SplitLine
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public CalendarLabel DayLabel
		{
			get;
			set;
		}

		public CalendarLabel MonthLabel
		{
			get;
			set;
		}

		public CalendarLabel YearLabel
		{
			get;
			set;
		}
	}
}
