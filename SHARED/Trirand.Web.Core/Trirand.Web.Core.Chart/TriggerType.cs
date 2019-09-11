using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum TriggerType
	{
		[EnumMember(Value = "Axis")]
		Axis,
		[EnumMember(Value = "Item")]
		Item,
		[EnumMember(Value = "None")]
		None
	}
}
