using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum LineStepType
	{
		[EnumMember(Value = "Start")]
		Start,
		[EnumMember(Value = "Middle")]
		Middle,
		[EnumMember(Value = "End")]
		End
	}
}
