using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum OrientType
	{
		[EnumMember(Value = "Horizontal")]
		Horizontal,
		[EnumMember(Value = "Vertical")]
		Vertical,
		[EnumMember(Value = "Radial")]
		Radial
	}
}
