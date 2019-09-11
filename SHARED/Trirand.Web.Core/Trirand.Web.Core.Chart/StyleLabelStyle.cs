using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum StyleLabelStyle
	{
		[EnumMember(Value = "Center")]
		Center,
		[EnumMember(Value = "Outer")]
		Outer,
		[EnumMember(Value = "Inner")]
		Inner,
		[EnumMember(Value = "Left")]
		Left,
		[EnumMember(Value = "Right")]
		Right,
		[EnumMember(Value = "Top")]
		Top,
		[EnumMember(Value = "Inside")]
		Inside,
		[EnumMember(Value = "Bottom")]
		Bottom,
		[EnumMember(Value = "InsideLeft")]
		InsideLeft,
		[EnumMember(Value = "InsideRight")]
		InsideRight,
		[EnumMember(Value = "InsideTop")]
		InsideTop,
		[EnumMember(Value = "InsideBottom")]
		InsideBottom,
		[EnumMember(Value = "Start")]
		Start,
		[EnumMember(Value = "Outside")]
		Outside
	}
}
