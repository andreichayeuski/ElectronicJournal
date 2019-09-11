using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum CoordinateSystemType
	{
		[EnumMember(Value = "Cartesian2d")]
		Cartesian2d,
		[EnumMember(Value = "Geo")]
		Geo,
		[EnumMember(Value = "Bmap")]
		Bmap,
		[EnumMember(Value = "Polar")]
		Polar,
		[EnumMember(Value = "Parallel")]
		Parallel
	}
}
