using System.Runtime.Serialization;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public enum ChartType
	{
		[EnumMember(Value = "Line")]
		Line,
		[EnumMember(Value = "Lines")]
		Lines,
		[EnumMember(Value = "Bar")]
		Bar,
		[EnumMember(Value = "Scatter")]
		Scatter,
		[EnumMember(Value = "EffectScatter")]
		EffectScatter,
		[EnumMember(Value = "K")]
		K,
		[EnumMember(Value = "Pie")]
		Pie,
		[EnumMember(Value = "Radar")]
		Radar,
		[EnumMember(Value = "Chord")]
		Chord,
		[EnumMember(Value = "Force")]
		Force,
		[EnumMember(Value = "Funnel")]
		Funnel,
		[EnumMember(Value = "Gauge")]
		Gauge,
		[EnumMember(Value = "Map")]
		Map,
		[EnumMember(Value = "Time")]
		Time,
		[EnumMember(Value = "Heatmap")]
		Heatmap,
		[EnumMember(Value = "EventRiver")]
		EventRiver,
		[EnumMember(Value = "Tree")]
		Tree,
		[EnumMember(Value = "Treemap")]
		Treemap,
		[EnumMember(Value = "Venn")]
		Venn,
		[EnumMember(Value = "WordCloud")]
		WordCloud,
		[EnumMember(Value = "Parallel")]
		Parallel,
		[EnumMember(Value = "Boxplot")]
		Boxplot,
		[EnumMember(Value = "Graph")]
		Graph,
		[EnumMember(Value = "Sankey")]
		Sankey,
		[EnumMember(Value = "PictorialBar")]
		PictorialBar,
		[EnumMember(Value = "ThemeRiver")]
		ThemeRiver
	}
}
