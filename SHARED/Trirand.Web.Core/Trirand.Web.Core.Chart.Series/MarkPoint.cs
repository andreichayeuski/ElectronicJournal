using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class MarkPoint : AbstractData<MarkPoint>
	{
		public ToolTip ToolTip
		{
			get;
			set;
		}

		public bool? Clickable
		{
			get;
			set;
		}

		public string Symbol
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
	}
}
