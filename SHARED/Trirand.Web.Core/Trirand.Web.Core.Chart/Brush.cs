using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class Brush : Basic<Brush>
	{
		public IList<BrushToolBoxType> Toolbox
		{
			get;
			set;
		}

		public object BrushLink
		{
			get;
			set;
		}

		public object SeriesIndex
		{
			get;
			set;
		}

		public object GeoIndex
		{
			get;
			set;
		}

		public BrushType? BrushType
		{
			get;
			set;
		}

		public BrushMode? BrushMode
		{
			get;
			set;
		}

		public bool? Transformable
		{
			get;
			set;
		}

		public BrushStyle BrushStyle
		{
			get;
			set;
		}

		public BrushThrottleType? ThrottleType
		{
			get;
			set;
		}

		public int? ThrottleDelay
		{
			get;
			set;
		}

		public bool? RemoveOnClick
		{
			get;
			set;
		}

		public VisualItem InBrush
		{
			get;
			set;
		}

		public VisualItem outBrush
		{
			get;
			set;
		}
	}
}
