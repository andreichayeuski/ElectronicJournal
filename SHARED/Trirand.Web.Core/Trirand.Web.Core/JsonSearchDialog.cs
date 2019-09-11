using System.Collections;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonSearchDialog
	{
		private Hashtable _jsonValues;

		private CoreGrid _grid;

		public Hashtable JsonValues
		{
			get
			{
				return _jsonValues;
			}
			set
			{
				_jsonValues = value;
			}
		}

		public JsonSearchDialog(CoreGrid grid)
		{
			_jsonValues = new Hashtable();
			_grid = grid;
		}

		public string Process()
		{
			SearchDialogSettings searchDialogSettings = _grid.SearchDialogSettings;
			if (searchDialogSettings.TopOffset != 0)
			{
				_jsonValues["top"] = searchDialogSettings.TopOffset;
			}
			if (searchDialogSettings.LeftOffset != 0)
			{
				_jsonValues["left"] = searchDialogSettings.LeftOffset;
			}
			if (searchDialogSettings.Width != 300)
			{
				_jsonValues["width"] = searchDialogSettings.Width;
			}
			if (searchDialogSettings.Height != 300)
			{
				_jsonValues["height"] = searchDialogSettings.Height;
			}
			if (searchDialogSettings.Modal)
			{
				_jsonValues["modal"] = true;
			}
			if (!searchDialogSettings.Draggable)
			{
				_jsonValues["drag"] = false;
			}
			if (!string.IsNullOrEmpty(searchDialogSettings.FindButtonText))
			{
				_jsonValues["Find"] = searchDialogSettings.FindButtonText;
			}
			if (!string.IsNullOrEmpty(searchDialogSettings.ResetButtonText))
			{
				_jsonValues["Clear"] = searchDialogSettings.ResetButtonText;
			}
			if (searchDialogSettings.MultipleSearch)
			{
				_jsonValues["multipleSearch"] = true;
			}
			if (searchDialogSettings.ValidateInput)
			{
				_jsonValues["checkInput"] = true;
			}
			if (!searchDialogSettings.Resizable)
			{
				_jsonValues["resize"] = false;
			}
			_jsonValues["recreateForm"] = true;
			return JsonConvert.SerializeObject((object)_jsonValues);
		}
	}
}
