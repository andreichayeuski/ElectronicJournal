using System.Collections;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonSearchToolBar
	{
		private CoreGrid _grid;

		public JsonSearchToolBar(CoreGrid grid)
		{
			_grid = grid;
		}

		public string Process()
		{
			Hashtable hashtable = new Hashtable();
			if (_grid.SearchToolBarSettings.SearchToolBarAction == SearchToolBarAction.SearchOnKeyPress)
			{
				hashtable["searchOnEnter"] = false;
			}

		    if (this._grid.SearchToolBarSettings.SearchOperators) //дописано из старой
		    {
		        hashtable["searchOperators"] = true;
		        hashtable["stringResult"] = true;
		    }

            string text = JsonConvert.SerializeObject((object)hashtable);
			ClientSideEvents clientSideEvents = _grid.ClientSideEvents;
			if (!string.IsNullOrEmpty(clientSideEvents.BeforeSearch))
			{
				text = JsonUtil.RenderClientSideEvent(text, "beforeSearch", clientSideEvents.BeforeSearch);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.AfterSearch))
			{
				text = JsonUtil.RenderClientSideEvent(text, "afterSearch", clientSideEvents.AfterSearch);
			}
			return text;
		}
	}
}
