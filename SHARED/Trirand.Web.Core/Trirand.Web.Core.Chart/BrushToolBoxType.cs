using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum BrushToolBoxType
	{
		[EnumMember(Value = "Rect")]
		Rect,
		[EnumMember(Value = "Polygon")]
		Polygon,
		[EnumMember(Value = "LineX")]
		LineX,
		[EnumMember(Value = "LineY")]
		LineY,
		[EnumMember(Value = "Keep")]
		Keep,
		[EnumMember(Value = "Clear")]
		Clear
	}
}
