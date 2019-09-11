using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Force : ChartSeries<Force>
	{
		public IList<Category> Categories
		{
			get;
			set;
		}

		public object Nodes
		{
			get;
			set;
		}

		public object Links
		{
			get;
			set;
		}

		public int[,] Matrix
		{
			get;
			set;
		}

		public object Center
		{
			get;
			set;
		}

		public int? Size
		{
			get;
			set;
		}

		public object MinRadius
		{
			get;
			set;
		}

		public object MaxRadius
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

		public object LinkSymbol
		{
			get;
			set;
		}

		public double? CoolDown
		{
			get;
			set;
		}

		public IList<int> LinkSymbolSize
		{
			get;
			set;
		}

		public double? Scaling
		{
			get;
			set;
		}

		public double? Gravity
		{
			get;
			set;
		}

		public bool? Draggable
		{
			get;
			set;
		}

		public bool? Large
		{
			get;
			set;
		}

		public bool? UseWorker
		{
			get;
			set;
		}

		public int? Steps
		{
			get;
			set;
		}

		public object Roam
		{
			get;
			set;
		}

		public bool RibbonType
		{
			get;
			set;
		}

		public Force()
		{
			base.Type = ChartType.Force;
		}

		public Force(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
