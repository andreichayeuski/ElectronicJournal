using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class ToolBarSettings
	{
		public bool ShowEditButton
		{
			get;
			set;
		}

		public bool ShowAddButton
		{
			get;
			set;
		}

		public bool ShowDeleteButton
		{
			get;
			set;
		}

		public bool ShowSearchButton
		{
			get;
			set;
		}

		public bool ShowRefreshButton
		{
			get;
			set;
		}

		public bool ShowViewRowDetailsButton
		{
			get;
			set;
		}

		public bool ShowSearchToolBar
		{
			get;
			set;
		}

		public ToolBarPosition ToolBarPosition
		{
			get;
			set;
		}

		public ToolBarAlign ToolBarAlign
		{
			get;
			set;
		}

		public List<CoreGridToolBarButton> CustomButtons
		{
			get;
			set;
		}

		public ToolBarSettings()
		{
			ShowEditButton = false;
			ShowAddButton = false;
			ShowDeleteButton = false;
			ShowSearchButton = false;
			ShowRefreshButton = false;
			ShowViewRowDetailsButton = false;
			ShowSearchToolBar = false;
			ToolBarAlign = ToolBarAlign.Left;
			ToolBarPosition = ToolBarPosition.Bottom;
			CustomButtons = new List<CoreGridToolBarButton>();
		}
	}
}
