using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum PositionType
	{
		[EnumMember(Value = "Bottom")]
		Bottom,
		[EnumMember(Value = "Top")]
		Top,
		[EnumMember(Value = "Left")]
		Left,
		[EnumMember(Value = "Right")]
		Right,
		[EnumMember(Value = "None")]
		None
	}
}
