namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class GroupField
	{
		public string DataField
		{
			get;
			set;
		}

		public string HeaderText
		{
			get;
			set;
		}

		public bool ShowGroupColumn
		{
			get;
			set;
		}

		public SortDirection GroupSortDirection
		{
			get;
			set;
		}

		public bool ShowGroupSummary
		{
			get;
			set;
		}

		public GroupField()
		{
			DataField = "";
			HeaderText = "<b>{0}</b>";
			ShowGroupColumn = true;
			GroupSortDirection = SortDirection.Asc;
			ShowGroupSummary = false;
		}
	}
}
