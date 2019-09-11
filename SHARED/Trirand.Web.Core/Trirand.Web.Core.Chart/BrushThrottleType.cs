using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum BrushThrottleType
	{
		[EnumMember(Value = "Debounce")]
		Debounce,
		[EnumMember(Value = "FixRate")]
		FixRate
	}
}
