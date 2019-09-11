using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum DataZoomType
	{
		[EnumMember(Value = "Inside")]
		Inside,
		[EnumMember(Value = "Slider")]
		Slider
	}
}
