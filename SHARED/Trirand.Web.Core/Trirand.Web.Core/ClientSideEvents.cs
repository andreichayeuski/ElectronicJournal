namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class ClientSideEvents
	{
		public string BeforeSearch
		{
			get;
			set;
		}

		public string AfterSearch
		{
			get;
			set;
		}

		public string BeforeAddDialogShown
		{
			get;
			set;
		}

		public string AfterAddDialogShown
		{
			get;
			set;
		}

		public string AfterAddDialogRowInserted
		{
			get;
			set;
		}
        

        public string BeforeEditDialogShown
		{
			get;
			set;
		}

		public string AfterEditDialogShown
		{
			get;
			set;
		}

		public string AfterEditDialogRowInserted
		{
			get;
			set;
		}

		public string BeforeDeleteDialogShown
		{
			get;
			set;
		}

		public string AfterDeleteDialogShown
		{
			get;
			set;
		}

		public string AfterDeleteDialogRowDeleted
		{
			get;
			set;
		}

		public string RowSelect
		{
			get;
			set;
		}

		public string RowDoubleClick
		{
			get;
			set;
		}

		public string RowRightClick
		{
			get;
			set;
		}

		public string GridInitialized
		{
			get;
			set;
		}

		public string BeforeAjaxRequest
		{
			get;
			set;
		}

		public string AfterAjaxRequest
		{
			get;
			set;
		}

		public string ServerError
		{
			get;
			set;
		}

		public string LoadDataError
		{
			get;
			set;
		}

		public string SubGridRowExpanded
		{
			get;
			set;
		}

		public string ColumnSort
		{
			get;
			set;
		}


        public ClientSideEvents()
		{
			AfterAddDialogShown = "";
			AfterAddDialogRowInserted = "";
			AfterEditDialogShown = "";
			AfterEditDialogRowInserted = "";
			AfterDeleteDialogShown = "";
			AfterDeleteDialogRowDeleted = "";
			AfterAjaxRequest = "";
			AfterSearch = "";
			BeforeAddDialogShown = "";
			BeforeEditDialogShown = "";
			BeforeDeleteDialogShown = "";
			BeforeAjaxRequest = "";
			BeforeSearch = "";
			RowSelect = "";
			RowDoubleClick = "";
			RowRightClick = "";
			GridInitialized = "";
			ServerError = "";
			LoadDataError = "";
			SubGridRowExpanded = "";
			ColumnSort = "";
		}
	}
}
