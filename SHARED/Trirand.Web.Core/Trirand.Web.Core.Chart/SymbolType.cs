using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum SymbolType
	{
		[EnumMember(Value = "None")]
		None,
		[EnumMember(Value = "Auto")]
		Auto,
		[EnumMember(Value = "Circle")]
		Circle,
		[EnumMember(Value = "Rectangle")]
		Rectangle,
		[EnumMember(Value = "Triangle")]
		Triangle,
		[EnumMember(Value = "Diamond")]
		Diamond,
		[EnumMember(Value = "EmptyCircle")]
		EmptyCircle,
		[EnumMember(Value = "EmptyRectangle")]
		EmptyRectangle,
		[EnumMember(Value = "EmptyTriangle")]
		EmptyTriangle,
		[EnumMember(Value = "EmptyDiamond")]
		EmptyDiamond,
		[EnumMember(Value = "Heart")]
		Heart,
		[EnumMember(Value = "Droplet")]
		Droplet,
		[EnumMember(Value = "Pin")]
		Pin,
		[EnumMember(Value = "Arrow")]
		Arrow,
		[EnumMember(Value = "Star")]
		Star,
		[EnumMember(Value = "Emptyheart")]
		Emptyheart,
		[EnumMember(Value = "Emptydroplet")]
		Emptydroplet,
		[EnumMember(Value = "Emptypin")]
		Emptypin,
		[EnumMember(Value = "Emptyarrow")]
		Emptyarrow,
		[EnumMember(Value = "Emptystar")]
		Emptystar
	}
}
