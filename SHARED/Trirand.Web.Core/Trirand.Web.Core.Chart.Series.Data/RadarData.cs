using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series.Data
{
	public class RadarData
	{
		public object Value
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public object Symbol
		{
			get;
			set;
		}

		public object SymbolSize
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public RadarData(string name)
		{
			Name = name;
		}

		public RadarData(object value, string name)
		{
			Value = value;
			Name = name;
		}

		public RadarData(object value, string name, ItemStyle itemStyle)
			: this(value, name)
		{
			ItemStyle = itemStyle;
		}
	}
}
