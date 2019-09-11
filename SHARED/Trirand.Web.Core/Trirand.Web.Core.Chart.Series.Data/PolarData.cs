using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series.Data
{
	public class PolarData
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

		public int? SymbolRotate
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

		public PolarData(string name)
		{
			Name = name;
		}

		public PolarData(object value, string name)
		{
			Value = value;
			Name = name;
		}

		public PolarData(object value, string name, ItemStyle itemStyle)
			: this(value, name)
		{
			ItemStyle = itemStyle;
		}
	}
}
