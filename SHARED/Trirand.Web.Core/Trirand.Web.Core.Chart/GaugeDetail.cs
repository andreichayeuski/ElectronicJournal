using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class GaugeDetail
	{
		public bool? Show
		{
			get;
			set;
		}

		public string BackgroundColor
		{
			get;
			set;
		}

		public int? BorderWidth
		{
			get;
			set;
		}

		public string BorderColor
		{
			get;
			set;
		}

		public int? Width
		{
			get;
			set;
		}

		public int? Height
		{
			get;
			set;
		}

		public IList<string> OffsetCenter
		{
			get;
			set;
		}

		public object Formatter
		{
			get;
			set;
		}

		public TextStyle TextStyle
		{
			get;
			set;
		}

		public int? ShadowBlur
		{
			get;
			set;
		}

		public object ShadowColor
		{
			get;
			set;
		}
	}
}
