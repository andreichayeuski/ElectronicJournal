using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class VisualItem
	{
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

		public object Color
		{
			get;
			set;
		}

		public double? ColorAlpha
		{
			get;
			set;
		}

		public double? Opacity
		{
			get;
			set;
		}

		public IList<double> ColorLightness
		{
			get;
			set;
		}

		public double? ColorStaturation
		{
			get;
			set;
		}

		public double? ColorHue
		{
			get;
			set;
		}
	}
}
