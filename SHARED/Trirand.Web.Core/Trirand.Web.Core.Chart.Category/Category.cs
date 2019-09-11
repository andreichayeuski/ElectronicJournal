namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Category
{
	public class Category<T> : XCategory where T : class
	{
		public string Name
		{
			get;
			set;
		}

		public string NameLocation
		{
			get;
			set;
		}

		public string Position
		{
			get;
			set;
		}

		public int? SplitNumber
		{
			get;
			set;
		}
	}
}
