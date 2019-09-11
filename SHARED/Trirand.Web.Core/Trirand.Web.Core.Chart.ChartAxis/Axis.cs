using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public abstract class Axis : AbstractData<Axis>
	{
		public TextStyle NameTextStyle
		{
			get;
			set;
		}

		public AxisLine AxisLine
		{
			get;
			set;
		}

		public AxisTick AxisTick
		{
			get;
			set;
		}

		public AxisLabel AxisLabel
		{
			get;
			set;
		}

		public SplitLine SplitLine
		{
			get;
			set;
		}

		public SplitArea SplitArea
		{
			get;
			set;
		}

		public AxisPointer AxisPointer
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public object Position
		{
			get;
			set;
		}

		public int ZLevel
		{
			get;
			set;
		}

		public int? GridIndex
		{
			get;
			set;
		}

		public int? NameGap
		{
			get;
			set;
		}

		public bool? Inverse
		{
			get;
			set;
		}

		public bool Scale
		{
			get;
			set;
		}
	}
}
