using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum SortType
	{
		[EnumMember(Value = "None")]
		None,
		[EnumMember(Value = "Ascending")]
		Ascending,
		[EnumMember(Value = "Descending")]
		Descending
	}
}
