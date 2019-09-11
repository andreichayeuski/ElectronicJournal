using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum TriggerOnType
	{
		[EnumMember(Value = "Mousemove")]
		Mousemove,
		[EnumMember(Value = "Click")]
		Click,
		[EnumMember(Value = "None")]
		None
	}
}
