namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Style
{
	public abstract class Style<T> where T : class
	{
		public bool? Show
		{
			get;
			set;
		}

		public object Color
		{
			get;
			set;
		}

		public object Color0
		{
			get;
			set;
		}

		public LineStyle LineStyle
		{
			get;
			set;
		}

		public AreaStyle AreaStyle
		{
			get;
			set;
		}

		public ChordStyle ChordStyle
		{
			get;
			set;
		}

		public NodeStyle NodeStyle
		{
			get;
			set;
		}

		public LinkStyle LinkStyle
		{
			get;
			set;
		}

		public TextStyle TextStyle
		{
			get;
			set;
		}

		public object BorderColor
		{
			get;
			set;
		}

		public double? BorderWidth
		{
			get;
			set;
		}

		public string BarBorderColor
		{
			get;
			set;
		}

		public object BarBorderRadius
		{
			get;
			set;
		}

		public int? BarBorderWidth
		{
			get;
			set;
		}

		public string BrushType
		{
			get;
			set;
		}

		public object Position
		{
			get;
			set;
		}

		public double[] Offset
		{
			get;
			set;
		}

		public object Formatter
		{
			get;
			set;
		}

		public StyleLabel Label
		{
			get;
			set;
		}

		public LineStyleType? BorderType
		{
			get;
			set;
		}

		public LabelLine LabelLine
		{
			get;
			set;
		}

		public int? ShadowBlur
		{
			get;
			set;
		}

		public string ShadowColor
		{
			get;
			set;
		}

		public int? ShadowOffsetX
		{
			get;
			set;
		}

		public int? ShadowOffsetY
		{
			get;
			set;
		}

		public string AreaColor
		{
			get;
			set;
		}

		public double? Width
		{
			get;
			set;
		}

		public double? Opacity
		{
			get;
			set;
		}

		public double? Curveness
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}
	}
}
