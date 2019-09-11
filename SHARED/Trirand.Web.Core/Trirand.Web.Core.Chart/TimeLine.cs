using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class TimeLine : Basic<TimeLine>, IData<TimeLine>
	{
		public TimeType? Type
		{
			get;
			set;
		}

		public AxisType? AxisType
		{
			get;
			set;
		}

		public bool? NotMerge
		{
			get;
			set;
		}

		public bool? Realtime
		{
			get;
			set;
		}

		public object X2
		{
			get;
			set;
		}

		public object Y2
		{
			get;
			set;
		}

		public new bool? Inverse
		{
			get;
			set;
		}

		public new OrientType? Orient
		{
			get;
			set;
		}

		public PositionType? ControlPosition
		{
			get;
			set;
		}

		public bool? AutoPlay
		{
			get;
			set;
		}

		public bool? Loop
		{
			get;
			set;
		}

		public int? PlayInterval
		{
			get;
			set;
		}

		public LineStyle LineStyle
		{
			get;
			set;
		}

		public EntityStyle<StyleLabel> Label
		{
			get;
			set;
		}

		public CheckPointStyle CheckpointStyle
		{
			get;
			set;
		}

		public ControlStyle ControlStyle
		{
			get;
			set;
		}

		public string Symbol
		{
			get;
			set;
		}

		public int? SymbolSize
		{
			get;
			set;
		}

		public int? CurrentIndex
		{
			get;
			set;
		}

		public IList<object> Data
		{
			get;
			set;
		}
	}
}
