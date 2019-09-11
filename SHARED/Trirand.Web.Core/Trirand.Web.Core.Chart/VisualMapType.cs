using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum VisualMapType
	{
		[EnumMember(Value = "Continuous")]
		Continuous,
		[EnumMember(Value = "Piecewise")]
		Piecewise
	}
}
