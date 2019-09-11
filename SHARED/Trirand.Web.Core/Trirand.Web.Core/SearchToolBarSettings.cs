namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class SearchToolBarSettings
	{
		public SearchToolBarAction SearchToolBarAction
		{
			get;
			set;
		}

		public SearchToolBarSettings()
		{
			SearchToolBarAction = SearchToolBarAction.SearchOnEnter;
		}

	    public bool SearchOperators { get; set; }
    }
}
