using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series.Data
{
	public class PieData<T>
	{
		public T Value
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public ItemStyle ItemStyle
		{
			get;
			set;
		}

		public PieData()
		{
		}

		public PieData(T value, string name)
		{
			Value = value;
			Name = name;
		}

		public PieData(T value, string name, ItemStyle itemStyle)
			: this(value, name)
		{
			ItemStyle = itemStyle;
		}
	}
}
