using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartFeature;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class ToolBox : Basic<ToolBox>
	{
		public new OrientType? Orient
		{
			get;
			set;
		}

		public new int? ItemGap
		{
			get;
			set;
		}

		public int? ItemSize
		{
			get;
			set;
		}

		public IList<string> Color
		{
			get;
			set;
		}

		public string DisableColor
		{
			get;
			set;
		}

		public string EffectiveColor
		{
			get;
			set;
		}

		public bool? ShowTitle
		{
			get;
			set;
		}

		public TextStyle TextStyle
		{
			get;
			set;
		}

		public Feature Feature
		{
			get;
			set;
		}

		public ItemStyle IconStyle
		{
			get;
			set;
		}
	}
}
