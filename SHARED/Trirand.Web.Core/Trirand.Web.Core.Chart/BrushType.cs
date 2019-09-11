using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum BrushType
	{
		[EnumMember(Value = "Fill")]
		Fill,
		[EnumMember(Value = "Stroke")]
		Stroke,
		[EnumMember(Value = "Rect")]
		Rect,
		[EnumMember(Value = "Polygon")]
		Polygon,
		[EnumMember(Value = "LineX")]
		LineX,
		[EnumMember(Value = "LineY")]
		LineY,
		[EnumMember(Value = "Clear")]
		Clear
	}
}
