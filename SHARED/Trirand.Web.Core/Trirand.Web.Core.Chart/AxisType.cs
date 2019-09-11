using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum AxisType
	{
		[EnumMember(Value = "Category")]
		Category,
		[EnumMember(Value = "Value")]
		Value,
		[EnumMember(Value = "Time")]
		Time,
		[EnumMember(Value = "Log")]
		Log
	}
}
