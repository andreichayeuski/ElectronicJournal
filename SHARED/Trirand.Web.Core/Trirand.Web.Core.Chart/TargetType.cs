using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum TargetType
	{
		[EnumMember(Value = "self")]
		Self,
		[EnumMember(Value = "blank")]
		Blank
	}
}
