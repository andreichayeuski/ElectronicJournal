namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public abstract class Basic<T> where T : class
	{
		public string Name
		{
			get;
			set;
		}

		public bool? Show
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

		public object Padding
		{
			get;
			set;
		}

		public object BorderWidth
		{
			get;
			set;
		}

		public string BackgroundColor
		{
			get;
			set;
		}

		public string BorderColor
		{
			get;
			set;
		}

		public object Left
		{
			get;
			set;
		}

		public object Right
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

		public OrientType Orient
		{
			get;
			set;
		}

		public object ShadowColor
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

		public object Width
		{
			get;
			set;
		}

		public object Height
		{
			get;
			set;
		}

		public int? ZLevel
		{
			get;
			set;
		}

		public int? Z
		{
			get;
			set;
		}

		public int? ItemGap
		{
			get;
			set;
		}

		public int? ShadowBlur
		{
			get;
			set;
		}

		public int? Min
		{
			get;
			set;
		}

		public object Max
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

		public int? AnimationDelay
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

		public double? Zoom
		{
			get;
			set;
		}

		public bool? Inverse
		{
			get;
			set;
		}

		public int? SplitNumber
		{
			get;
			set;
		}
	}
}
