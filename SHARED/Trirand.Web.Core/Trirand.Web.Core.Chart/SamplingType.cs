using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum SamplingType
	{
		[EnumMember(Value = "Max")]
		Max,
		[EnumMember(Value = "Min")]
		Min,
		[EnumMember(Value = "Average")]
		Average,
		[EnumMember(Value = "Sum")]
		Sum
	}
}
