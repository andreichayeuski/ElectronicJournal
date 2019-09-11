using System.Collections;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonAddDialog
	{
		private Hashtable _jsonValues;

		private CoreGrid _grid;

		public JsonAddDialog(CoreGrid grid)
		{
			_jsonValues = new Hashtable();
			_grid = grid;
		}

		public string Process()
		{
			AddDialogSettings addDialogSettings = _grid.AddDialogSettings;
			if (addDialogSettings.TopOffset != 0)
			{
				_jsonValues["top"] = addDialogSettings.TopOffset;
			}
			if (addDialogSettings.LeftOffset != 0)
			{
				_jsonValues["left"] = addDialogSettings.LeftOffset;
			}
			if (addDialogSettings.Width != 300)
			{
				_jsonValues["width"] = addDialogSettings.Width;
			}
			if (addDialogSettings.Height != 300)
			{
				_jsonValues["height"] = addDialogSettings.Height;
			}
			if (addDialogSettings.Modal)
			{
				_jsonValues["modal"] = true;
			}
			if (!addDialogSettings.Draggable)
			{
				_jsonValues["drag"] = false;
			}
			if (!string.IsNullOrEmpty(addDialogSettings.Caption))
			{
				_jsonValues["addCaption"] = addDialogSettings.Caption;
			}
			if (!string.IsNullOrEmpty(addDialogSettings.SubmitText))
			{
				_jsonValues["bSubmit"] = addDialogSettings.SubmitText;
			}
			if (!string.IsNullOrEmpty(addDialogSettings.CancelText))
			{
				_jsonValues["bCancel"] = addDialogSettings.CancelText;
			}
			if (!string.IsNullOrEmpty(addDialogSettings.LoadingMessageText))
			{
				_jsonValues["processData"] = addDialogSettings.LoadingMessageText;
			}
			if (addDialogSettings.CloseAfterAdding)
			{
				_jsonValues["closeAfterAdd"] = addDialogSettings.CloseAfterAdding;
			}
			if (!addDialogSettings.ClearAfterAdding)
			{
				_jsonValues["clearAfterAdd"] = false;
			}
			if (!addDialogSettings.ReloadAfterSubmit)
			{
				_jsonValues["reloadAfterSubmit"] = false;
			}
			if (!addDialogSettings.Resizable)
			{
				_jsonValues["resize"] = false;
			}
			_jsonValues["recreateForm"] = true;
			string json = JsonConvert.SerializeObject((object)_jsonValues);
			ClientSideEvents clientSideEvents = _grid.ClientSideEvents;
			json = JsonUtil.RenderClientSideEvent(json, "beforeShowForm", clientSideEvents.BeforeAddDialogShown);
			json = JsonUtil.RenderClientSideEvent(json, "afterShowForm", clientSideEvents.AfterAddDialogShown);
			json = JsonUtil.RenderClientSideEvent(json, "afterComplete", clientSideEvents.AfterAddDialogRowInserted);
			json = JsonUtil.RenderClientSideEvent(json, "errorTextFormat", "function(data) { return 'Error: ' + data.responseText }");
			return JsonUtil.RenderClientSideEvent(json, "editData", string.Format("{{ __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }}", _grid.ID));
		}
	}
}
