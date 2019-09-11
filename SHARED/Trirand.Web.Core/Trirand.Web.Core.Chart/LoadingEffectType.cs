using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum LoadingEffectType
	{
		[EnumMember(Value = "Spin")]
		Spin,
		[EnumMember(Value = "Bar")]
		Bar,
		[EnumMember(Value = "Ring")]
		Ring,
		[EnumMember(Value = "Whirling")]
		Whirling,
		[EnumMember(Value = "DynamicLine")]
		DynamicLine,
		[EnumMember(Value = "Bubble")]
		Bubble
	}
}
