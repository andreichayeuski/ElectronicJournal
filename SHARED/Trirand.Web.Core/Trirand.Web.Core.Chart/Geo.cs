using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class Geo : Basic<Geo>
	{
		public ChartType? Type
		{
			get;
			set;
		}

		public object Center
		{
			get;
			set;
		}

		public string Map
		{
			get;
			set;
		}

		public bool? Silent
		{
			get;
			set;
		}

		public bool? Roam
		{
			get;
			set;
		}

		public double AspectScale
		{
			get;
			set;
		}

		public ScaleLimit ScaleLimit
		{
			get;
			set;
		}

		public object NameMap
		{
			get;
			set;
		}

		public IList<string> LayoutCenter
		{
			get;
			set;
		}

		public object LayoutSize
		{
			get;
			set;
		}

		public Regions Regions
		{
			get;
			set;
		}

		public ItemStyle Label
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
