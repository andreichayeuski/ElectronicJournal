using System;
using System.Text;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class CoreAutoCompleteRenderer
	{
		private CoreAutoComplete _model;

		public string RenderHtml(CoreAutoComplete autoComplete)
		{
			_model = autoComplete;
			//if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
			//{
			//	return "This is a trial version of CoreSuite for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
			//}
			Guard.IsNotNullOrEmpty(_model.ID, "ID", "You need to set ID for this CoreAutoComplete instance.");
			if (_model.DisplayMode == AutoCompleteDisplayMode.Standalone)
			{
				return GetStandaloneJavascript();
			}
			return GetControlEditorJavascript();
		}

		private string GetStandaloneJavascript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<input type='text' id='{0}' name='{0}' />", _model.ID);
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.AppendFormat("jQuery('#{0}').autocomplete({{", _model.ID);
			stringBuilder.Append(GetStartupOptions());
			stringBuilder.Append("});");
			stringBuilder.Append("});");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		private string GetControlEditorJavascript()
		{
			return $"<script type='text/javascript'>var {_model.ID}_acid = {{ {GetStartupOptions()} }};</script>";
		}

		private string GetStartupOptions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("id: '{0}'", _model.ID);
			stringBuilder.AppendFormat(",source: '{0}'", _model.DataUrl);
			stringBuilder.AppendIfFalse(_model.AutoFocus, ",autoFocus: true");
			stringBuilder.AppendFormatIfTrue(_model.Delay != 300, ",delay: {0}", _model.Delay);
			stringBuilder.AppendIfFalse(_model.Enabled, ",disabled: true");
			stringBuilder.AppendFormatIfTrue(_model.MinLength != 1, ",minLength: {0}", _model.MinLength);
			return stringBuilder.ToString();
		}
	}
}
