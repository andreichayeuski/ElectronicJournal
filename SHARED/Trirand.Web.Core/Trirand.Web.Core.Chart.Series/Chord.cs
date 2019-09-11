using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Chord : ChartSeries<Chord>
	{
		public IList<Category> Categories
		{
			get;
			set;
		}

		public IList<Node> Nodes
		{
			get;
			set;
		}

		public IList<Link> Links
		{
			get;
			set;
		}

		public int[,] Matrix
		{
			get;
			set;
		}

		public bool? RibbonType
		{
			get;
			set;
		}

		public string Symbol
		{
			get;
			set;
		}

		public int? SymbolSize
		{
			get;
			set;
		}

		public int? MinRadius
		{
			get;
			set;
		}

		public int? MaxRadius
		{
			get;
			set;
		}

		public object Radius
		{
			get;
			set;
		}

		public object Center
		{
			get;
			set;
		}

		public bool? ShowScale
		{
			get;
			set;
		}

		public int? StartAngle
		{
			get;
			set;
		}

		public bool? ShowScaleText
		{
			get;
			set;
		}

		public int? Padding
		{
			get;
			set;
		}

		public SortType? Sort
		{
			get;
			set;
		}

		public SortType? SortSub
		{
			get;
			set;
		}

		public bool? ClockWise
		{
			get;
			set;
		}

		public Chord()
		{
			base.Type = ChartType.Chord;
		}

		public Chord(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
