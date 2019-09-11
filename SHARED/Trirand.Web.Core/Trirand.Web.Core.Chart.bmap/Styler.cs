namespace Trirand.Web.Core.Trirand.Web.Core.Chart.bmap
{
	public class Styler
	{
		public string color
		{
			get;
			set;
		}

		public VisibilityType? visibility
		{
			get;
			set;
		}

		public Styler Color(string color)
		{
			this.color = color;
			return this;
		}

		public Styler Visibility(VisibilityType visibility)
		{
			this.visibility = visibility;
			return this;
		}
	}
}
