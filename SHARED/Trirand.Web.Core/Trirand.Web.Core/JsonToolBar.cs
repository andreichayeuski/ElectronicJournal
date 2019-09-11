namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonToolBar
	{
		public bool edit
		{
			get;
			set;
		}

		public bool add
		{
			get;
			set;
		}

		public bool del
		{
			get;
			set;
		}

		public bool search
		{
			get;
			set;
		}

		public bool refresh
		{
			get;
			set;
		}

		public bool view
		{
			get;
			set;
		}

		public string position
		{
			get;
			set;
		}

		public bool cloneToTop
		{
			get;
			set;
		}

		public JsonToolBar(ToolBarSettings settings)
		{
			edit = settings.ShowEditButton;
			add = settings.ShowAddButton;
			del = settings.ShowDeleteButton;
			search = settings.ShowSearchButton;
			refresh = settings.ShowRefreshButton;
			view = settings.ShowViewRowDetailsButton;
			position = settings.ToolBarAlign.ToString().ToLower();
			cloneToTop = true;
		}
	}
}
