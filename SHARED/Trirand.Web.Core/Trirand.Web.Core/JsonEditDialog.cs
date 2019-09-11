using System.Collections;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonEditDialog
	{
		private Hashtable _jsonValues;

		private CoreGrid _grid;

		public JsonEditDialog(CoreGrid grid)
		{
			_jsonValues = new Hashtable();
			_grid = grid;
		}

		public string Process()
		{
			EditDialogSettings editDialogSettings = _grid.EditDialogSettings;
			if (editDialogSettings.TopOffset != 0)
			{
				_jsonValues["top"] = editDialogSettings.TopOffset;
			}
			if (editDialogSettings.LeftOffset != 0)
			{
				_jsonValues["left"] = editDialogSettings.LeftOffset;
			}
			if (editDialogSettings.Width != 300)
			{
				_jsonValues["width"] = editDialogSettings.Width;
			}
			if (editDialogSettings.Height != 300)
			{
				_jsonValues["height"] = editDialogSettings.Height;
			}
			if (editDialogSettings.Modal)
			{
				_jsonValues["modal"] = true;
			}
			if (!editDialogSettings.Draggable)
			{
				_jsonValues["drag"] = false;
			}
			if (!string.IsNullOrEmpty(editDialogSettings.Caption))
			{
				_jsonValues["editCaption"] = editDialogSettings.Caption;
			}
			if (!string.IsNullOrEmpty(editDialogSettings.SubmitText))
			{
				_jsonValues["bSubmit"] = editDialogSettings.SubmitText;
			}
			if (!string.IsNullOrEmpty(editDialogSettings.CancelText))
			{
				_jsonValues["bCancel"] = editDialogSettings.CancelText;
			}
			if (!string.IsNullOrEmpty(editDialogSettings.LoadingMessageText))
			{
				_jsonValues["processData"] = editDialogSettings.LoadingMessageText;
			}
			if (editDialogSettings.CloseAfterEditing)
			{
				_jsonValues["closeAfterEdit"] = true;
			}
			if (!editDialogSettings.ReloadAfterSubmit)
			{
				_jsonValues["reloadAfterSubmit"] = false;
			}
			if (!editDialogSettings.Resizable)
			{
				_jsonValues["resize"] = false;
			}
			_jsonValues["recreateForm"] = true;
			string json = JsonConvert.SerializeObject((object)_jsonValues);
			ClientSideEvents clientSideEvents = _grid.ClientSideEvents;
			json = JsonUtil.RenderClientSideEvent(json, "beforeShowForm", clientSideEvents.BeforeEditDialogShown);
			json = JsonUtil.RenderClientSideEvent(json, "afterShowForm", clientSideEvents.AfterEditDialogShown);
			json = JsonUtil.RenderClientSideEvent(json, "afterComplete", clientSideEvents.AfterEditDialogRowInserted);
			json = JsonUtil.RenderClientSideEvent(json, "errorTextFormat", "function(data) { return 'Error: ' + data.responseText }");
			return JsonUtil.RenderClientSideEvent(json, "editData", string.Format("{{ __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }}", _grid.ID));
		}
	}
}
