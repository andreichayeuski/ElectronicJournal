using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Data
{
	public abstract class BasicData<T> where T : class
	{
		public ToolTip Tooltip
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public object Value
		{
			get;
			set;
		}

		public object X
		{
			get;
			set;
		}

		public object Y
		{
			get;
			set;
		}

		public object XAxis
		{
			get;
			set;
		}

		public object YAxis
		{
			get;
			set;
		}

		public MarkType? Type
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

		public double? SymbolRotate
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public ItemStyle LineStyle
		{
			get;
			set;
		}

		public ItemStyle AreaStyle
		{
			get;
			set;
		}

		public EntityStyle<LineLabelStyle> Label
		{
			get;
			set;
		}

		public int? ValueIndex
		{
			get;
			set;
		}

		public IList<object> Coord
		{
			get;
			set;
		}
	}
}
