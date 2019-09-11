using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Pie : ChartSeries<Pie>
	{
		public IList<string> Center
		{
			get;
			set;
		}

		public object Radius
		{
			get;
			set;
		}

		public int? StartAngle
		{
			get;
			set;
		}

		public int? MinAngle
		{
			get;
			set;
		}

		public bool? ClockWise
		{
			get;
			set;
		}

		public NigRoseType? RoseType
		{
			get;
			set;
		}

		public int? SelectedOffset
		{
			get;
			set;
		}

		public SelectedModeType? SelectedMode
		{
			get;
			set;
		}

		public bool? LegendHoverLink
		{
			get;
			set;
		}

		public object X
		{
			get;
			set;
		}

		public object Y
		{
			get;
			set;
		}

		public object Width
		{
			get;
			set;
		}

		public HorizontalType? FunnelAlign
		{
			get;
			set;
		}

		public int? Max
		{
			get;
			set;
		}

		public SortType? Sort
		{
			get;
			set;
		}

		public Pie()
		{
			base.Type = ChartType.Pie;
		}

		public Pie(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
