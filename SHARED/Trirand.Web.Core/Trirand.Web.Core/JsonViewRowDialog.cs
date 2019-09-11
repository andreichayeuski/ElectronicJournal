using System.Collections;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonViewRowDialog
	{
		private Hashtable _jsonValues;

		private CoreGrid _grid;

		public JsonViewRowDialog(CoreGrid grid)
		{
			_jsonValues = new Hashtable();
			_grid = grid;
		}

		public string Process()
		{
			ViewRowDialogSettings viewRowDialogSettings = _grid.ViewRowDialogSettings;
			if (viewRowDialogSettings.TopOffset != 0)
			{
				_jsonValues["top"] = viewRowDialogSettings.TopOffset;
			}
			if (viewRowDialogSettings.LeftOffset != 0)
			{
				_jsonValues["left"] = viewRowDialogSettings.LeftOffset;
			}
			if (viewRowDialogSettings.Width != 300)
			{
				_jsonValues["width"] = viewRowDialogSettings.Width;
			}
			if (viewRowDialogSettings.Height != 300)
			{
				_jsonValues["height"] = viewRowDialogSettings.Height;
			}
			if (viewRowDialogSettings.Modal)
			{
				_jsonValues["modal"] = true;
			}
			if (!viewRowDialogSettings.Draggable)
			{
				_jsonValues["drag"] = false;
			}
			if (!string.IsNullOrEmpty(viewRowDialogSettings.Caption))
			{
				_jsonValues["editCaption"] = viewRowDialogSettings.Caption;
			}
			if (!string.IsNullOrEmpty(viewRowDialogSettings.SubmitText))
			{
				_jsonValues["bSubmit"] = viewRowDialogSettings.SubmitText;
			}
			if (!string.IsNullOrEmpty(viewRowDialogSettings.CancelText))
			{
				_jsonValues["bCancel"] = viewRowDialogSettings.CancelText;
			}
			if (!viewRowDialogSettings.Resizable)
			{
				_jsonValues["resize"] = false;
			}
			return JsonConvert.SerializeObject((object)_jsonValues);
		}
	}
}
