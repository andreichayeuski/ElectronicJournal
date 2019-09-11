using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum AxisPointType
	{
		[EnumMember(Value = "Line")]
		Line,
		[EnumMember(Value = "Cross")]
		Cross,
		[EnumMember(Value = "Shadow")]
		Shadow,
		[EnumMember(Value = "None")]
		None
	}
}
