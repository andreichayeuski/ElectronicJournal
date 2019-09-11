namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public abstract class AbstractData<T> : IData<T> where T : class
	{
		public object Data;

		private bool Clickable;

		private bool Hoverable;

		public string BlendMode
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
	}
}
