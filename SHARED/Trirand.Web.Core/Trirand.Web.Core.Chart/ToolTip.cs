using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class ToolTip : Basic<ToolTip>
	{
		public bool? ShowContent
		{
			get;
			set;
		}

		public TriggerType? Trigger
		{
			get;
			set;
		}

		public TriggerOnType TriggerOn
		{
			get;
			set;
		}

		public object Position
		{
			get;
			set;
		}

		public object Formatter
		{
			get;
			set;
		}

		public object IslandFormatter
		{
			get;
			set;
		}

		public int? ShowDelay
		{
			get;
			set;
		}

		public int? HideDelay
		{
			get;
			set;
		}

		public double? TransitionDuration
		{
			get;
			set;
		}

		public bool? Enterable
		{
			get;
			set;
		}

		public int? BorderRadius
		{
			get;
			set;
		}

		public AxisPointer AxisPointer
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
