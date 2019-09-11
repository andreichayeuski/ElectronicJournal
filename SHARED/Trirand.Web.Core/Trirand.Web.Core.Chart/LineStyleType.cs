using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum LineStyleType
	{
		[EnumMember(Value = "Solid")]
		Solid,
		[EnumMember(Value = "Dotted")]
		Dotted,
		[EnumMember(Value = "Dashed")]
		Dashed,
		[EnumMember(Value = "Curve")]
		Curve,
		[EnumMember(Value = "Broken")]
		Broken
	}
}
