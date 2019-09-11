namespace Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis
{
	public class ChartAxis<T> : Axis where T : class
	{
		public AxisType? Type
		{
			get;
			set;
		}

		public bool? Show
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

		public new string Name
		{
			get;
			set;
		}

		public NameLocationType? NameLocation
		{
			get;
			set;
		}

		public new string Position
		{
			get;
			set;
		}

		public int? SplitNumber
		{
			get;
			set;
		}

		public object Min
		{
			get;
			set;
		}

		public object Max
		{
			get;
			set;
		}

		public object Top
		{
			get;
			set;
		}

		public object Bottom
		{
			get;
			set;
		}

		public new bool? Scale
		{
			get;
			set;
		}

		public int? Interval
		{
			get;
			set;
		}
	}
}
