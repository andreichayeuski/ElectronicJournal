namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class PagerSettings
	{
		public int PageSize
		{
			get;
			set;
		}

		public int CurrentPage
		{
			get;
			set;
		}

		public string PageSizeOptions
		{
			get;
			set;
		}

		public string NoRowsMessage
		{
			get;
			set;
		}

		public bool ScrollBarPaging
		{
			get;
			set;
		}

		public string PagingMessage
		{
			get;
			set;
		}

		public PagerSettings()
		{
			PageSize = 10;
			CurrentPage = 1;
			PageSizeOptions = "[10,20,30]";
			NoRowsMessage = "";
			ScrollBarPaging = false;
			PagingMessage = "";
		}
	}
}
