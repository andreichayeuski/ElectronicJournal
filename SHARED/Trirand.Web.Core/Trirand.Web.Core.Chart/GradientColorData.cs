namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class GradientColorData
	{
		public double? Offset
		{
			get;
			set;
		}

		public string Color
		{
			get;
			set;
		}

		public GradientColorData(double offset, string color)
		{
			Offset = offset;
			Color = color;
		}
	}
}
