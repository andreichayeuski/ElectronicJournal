using Trirand.Web.Core.Trirand.Web.Core.Chart.Series.mark;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public abstract class Series
	{
		public bool? AvoidLabelOverlap
		{
			get;
			set;
		}

		public object Color
		{
			get;
			set;
		}

		public string CoordinateSystem
		{
			get;
			set;
		}

		public string Id
		{
			get;
			set;
		}

		public int? Zlevel
		{
			get;
			set;
		}

		public int? Z
		{
			get;
			set;
		}

		public ChartType Type
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public ToolTip Tooltip
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public EntityStyle<AreaStyle> AreaStyle
		{
			get;
			set;
		}

		public MarkPoint MarkPoint
		{
			get;
			set;
		}

		public MarkLine MarkLine
		{
			get;
			set;
		}

		public MarkArea MarkArea
		{
			get;
			set;
		}

		public ItemStyle Label
		{
			get;
			set;
		}

		public ItemStyle LabelLine
		{
			get;
			set;
		}

		public ItemStyle LineStyle
		{
			get;
			set;
		}

		public bool? Silent
		{
			get;
			set;
		}

		public bool? Animation
		{
			get;
			set;
		}

		public int? AnimationThreshold
		{
			get;
			set;
		}

		public int? AnimationDuration
		{
			get;
			set;
		}

		public string AnimationEasing
		{
			get;
			set;
		}

		public object AnimationDelay
		{
			get;
			set;
		}

		public int? AnimationDurationUpdate
		{
			get;
			set;
		}

		public string AnimationEasingUpdate
		{
			get;
			set;
		}

		public int? AnimationDelayUpdate
		{
			get;
			set;
		}

		public string BlendMode
		{
			get;
			set;
		}

		public bool? HoverAnimation
		{
			get;
			set;
		}
	}
}
