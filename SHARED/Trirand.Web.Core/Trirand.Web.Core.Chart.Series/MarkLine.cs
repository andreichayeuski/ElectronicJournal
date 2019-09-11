using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class MarkLine : AbstractData<MarkPoint>
	{
		public bool? Clickable
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

		public object SymbolRotate
		{
			get;
			set;
		}

		public bool? Large
		{
			get;
			set;
		}

		public bool? Smooth
		{
			get;
			set;
		}

		public double? Smoothness
		{
			get;
			set;
		}

		public int? Precision
		{
			get;
			set;
		}

		public Bound Bounding
		{
			get;
			set;
		}

		public Effect Effect
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public ItemStyle Label
		{
			get;
			set;
		}

		public EntityStyle<LineStyle> LineStyle
		{
			get;
			set;
		}

		public ToolTip ToolTip
		{
			get;
			set;
		}
	}
}
