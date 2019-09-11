using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series.Data
{
	public class SeriesData<T>
	{
		public T Value
		{
			get;
			set;
		}

		public IList<SeriesData<T>> Children
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public ToolTip Tooltip
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
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

		public StyleLabel Label
		{
			get;
			set;
		}

		public SeriesData(T value)
			: this(value, (ToolTip)null, (ItemStyle)null)
		{
		}

		public SeriesData(T value, string name)
			: this(value)
		{
			Name = name;
		}

		public SeriesData(T value, ToolTip tooltip)
			: this(value, tooltip, (ItemStyle)null)
		{
		}

		public SeriesData(T value, ItemStyle itemStyle)
			: this(value, (ToolTip)null, itemStyle)
		{
		}

		public SeriesData(T value, ToolTip tooltip, ItemStyle itemStyle)
		{
			Value = value;
			Tooltip = tooltip;
			ItemStyle = itemStyle;
		}
	}
}
