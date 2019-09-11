using System.Collections;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonDelDialog
	{
		private Hashtable _jsonValues;

		private CoreGrid _grid;

		public JsonDelDialog(CoreGrid grid)
		{
			_jsonValues = new Hashtable();
			_grid = grid;
		}

		public string Process()
		{
			DeleteDialogSettings deleteDialogSettings = _grid.DeleteDialogSettings;
			if (deleteDialogSettings.TopOffset != 0)
			{
				_jsonValues["top"] = deleteDialogSettings.TopOffset;
			}
			if (deleteDialogSettings.LeftOffset != 0)
			{
				_jsonValues["left"] = deleteDialogSettings.LeftOffset;
			}
			if (deleteDialogSettings.Width != 300)
			{
				_jsonValues["width"] = deleteDialogSettings.Width;
			}
			if (deleteDialogSettings.Height != 300)
			{
				_jsonValues["height"] = deleteDialogSettings.Height;
			}
			if (deleteDialogSettings.Modal)
			{
				_jsonValues["modal"] = true;
			}
			if (!deleteDialogSettings.Draggable)
			{
				_jsonValues["drag"] = false;
			}
			if (!string.IsNullOrEmpty(deleteDialogSettings.SubmitText))
			{
				_jsonValues["bSubmit"] = deleteDialogSettings.SubmitText;
			}
			if (!string.IsNullOrEmpty(deleteDialogSettings.CancelText))
			{
				_jsonValues["bCancel"] = deleteDialogSettings.CancelText;
			}
			if (!string.IsNullOrEmpty(deleteDialogSettings.LoadingMessageText))
			{
				_jsonValues["processData"] = deleteDialogSettings.LoadingMessageText;
			}
			if (!string.IsNullOrEmpty(deleteDialogSettings.Caption))
			{
				_jsonValues["caption"] = deleteDialogSettings.Caption;
			}
			if (!string.IsNullOrEmpty(deleteDialogSettings.DeleteMessage))
			{
				_jsonValues["msg"] = deleteDialogSettings.DeleteMessage;
			}
			if (!deleteDialogSettings.ReloadAfterSubmit)
			{
				_jsonValues["reloadAfterSubmit"] = false;
			}
			if (!deleteDialogSettings.Resizable)
			{
				_jsonValues["resize"] = false;
			}
			_jsonValues["recreateForm"] = true;
			string json = JsonConvert.SerializeObject((object)_jsonValues);
			ClientSideEvents clientSideEvents = _grid.ClientSideEvents;
			json = JsonUtil.RenderClientSideEvent(json, "beforeShowForm", clientSideEvents.BeforeDeleteDialogShown);
			json = JsonUtil.RenderClientSideEvent(json, "afterShowForm", clientSideEvents.AfterDeleteDialogShown);
			json = JsonUtil.RenderClientSideEvent(json, "afterComplete", clientSideEvents.AfterDeleteDialogRowDeleted);
			json = JsonUtil.RenderClientSideEvent(json, "errorTextFormat", "function(data) { return 'Error: ' + data.responseText }");
			return JsonUtil.RenderClientSideEvent(json, "delData", string.Format("{{ __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }}", _grid.ID));
		}
	}
}
