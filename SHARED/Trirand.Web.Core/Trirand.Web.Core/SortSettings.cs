namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class SortSettings
	{
		public bool AutoSortByPrimaryKey
		{
			get;
			set;
		}

		public string InitialSortColumn
		{
			get;
			set;
		}

		public SortDirection InitialSortDirection
		{
			get;
			set;
		}

		public SortIconsPosition SortIconsPosition
		{
			get;
			set;
		}

		public SortAction SortAction
		{
			get;
			set;
		}

		public bool MultiColumnSorting
		{
			get;
			set;
		}

		public SortSettings()
		{
			AutoSortByPrimaryKey = true;
			InitialSortColumn = "";
			InitialSortDirection = SortDirection.Asc;
			SortIconsPosition = SortIconsPosition.Vertical;
			SortAction = SortAction.ClickOnHeader;
			MultiColumnSorting = false;
		}
	}
}
