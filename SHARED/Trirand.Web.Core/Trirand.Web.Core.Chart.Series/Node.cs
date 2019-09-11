using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class Node
	{
		public string Name
		{
			get;
			set;
		}

		public string Label
		{
			get;
			set;
		}

		public int? Value
		{
			get;
			set;
		}

		public bool? Ignore
		{
			get;
			set;
		}

		public bool? Draggable
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

		public int? Category
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public Node()
		{
		}

		public Node(string name)
		{
			Name = name;
		}

		public Node(string name, int value, int category)
			: this(name)
		{
			Value = value;
			Category = category;
		}
	}
}
