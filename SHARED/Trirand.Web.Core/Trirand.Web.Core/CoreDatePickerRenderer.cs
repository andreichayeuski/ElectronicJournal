using System;
using System.Text;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class CoreDatePickerRenderer
	{
		private CoreDatePicker _model;

		public string RenderHtml(CoreDatePicker datePicker)
		{
			_model = datePicker;
			//if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
			//{
			//	return "This is a 30-day trial version of CoreSuite for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
			//}
			Guard.IsNotNullOrEmpty(_model.ID, "ID", "You need to set ID for this CoreDatePicker instance.");
			if (_model.DisplayMode == DatePickerDisplayMode.Standalone)
			{
				return GetStandaloneJavascript();
			}
			return GetControlEditorJavascript();
		}

		private string GetStandaloneJavascript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<input type='text' id='{0}' name='{0}' />", _model.ID);
			stringBuilder.Append("<style type='text/css'>body .ui-datepicker { z-index: 2000 }</style>");
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.AppendFormat("jQuery('#{0}').datepicker({{", _model.ID);
			stringBuilder.Append(GetStartupOptions());
			stringBuilder.Append("});");
			stringBuilder.Append("});");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		private string GetControlEditorJavascript()
		{
			return "<style type='text/css'>body .ui-datepicker { z-index: 2000 }</style>" + $"<script type='text/javascript'>var {_model.ID}_dpid = {{ {GetStartupOptions()} }};</script>";
		}

		private string GetStartupOptions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("id: '{0}'", _model.ID);
			stringBuilder.AppendFormatIfNotNullOrEmpty(_model.AltField, ",altField: '{0}'");
			stringBuilder.AppendFormatIfNotNullOrEmpty(_model.AltFormat, ",altFormat: '{0}'");
			stringBuilder.AppendFormatIfNotNullOrEmpty(_model.AppendText, ",appendText: '{0}'");
			stringBuilder.AppendIfTrue(_model.AutoSize, ",autoSize: true");
			stringBuilder.AppendFormatIfNotNullOrEmpty(_model.ButtonImage, ",buttonImage: '{0}'");
			stringBuilder.AppendIfTrue(_model.ButtonImageOnly, ",buttonImageOnly: true");
			stringBuilder.AppendFormatIfTrue(_model.ButtonText != "...", ",buttonText: '{0}'", _model.ButtonText);
			stringBuilder.AppendIfTrue(_model.ChangeMonth, ",changeMonth: true");
			stringBuilder.AppendIfTrue(_model.ChangeYear, ",changeYear: true");
			stringBuilder.AppendFormatIfTrue(_model.CloseText != "Done", ",closeText: '{0}'", _model.CloseText);
			stringBuilder.AppendIfFalse(_model.ConstrainInput, ",constrainInput: false");
			stringBuilder.AppendFormatIfTrue(_model.CurrentText != "Today", ",currentText: '{0}'", _model.CurrentText);
			stringBuilder.AppendFormat(",dateFormat: '{0}'", ConvertDotNetDateFormatToJquery(_model.DateFormat));
			stringBuilder.AppendFormatIfTrue(_model.DayNames.Count > 0, ",dayNames: {0}", JsonConvert.SerializeObject((object)_model.DayNames));
			stringBuilder.AppendFormatIfTrue(_model.DayNamesMin.Count > 0, ",dayNamesMin: {0}", JsonConvert.SerializeObject((object)_model.DayNamesMin));
			stringBuilder.AppendFormatIfTrue(_model.DayNamesShort.Count > 0, ",dayNamesShort: {0}", JsonConvert.SerializeObject((object)_model.DayNamesShort));
			stringBuilder.AppendFormatIfTrue(_model.DefaultDate.HasValue, ",defaultDate: {0}", FormatDateToJavascript(_model.DefaultDate));
			stringBuilder.AppendFormatIfTrue(_model.Duration != 500, ",duration: {0}", _model.Duration.ToString());
			stringBuilder.AppendIfFalse(_model.Enabled, ",disabled: true");
			stringBuilder.AppendFormatIfTrue(_model.FirstDay != 0, ",firstDay: {0}", _model.FirstDay.ToString());
			stringBuilder.AppendIfTrue(_model.GotoCurrent, ",gotoCurrent: true");
			stringBuilder.AppendIfTrue(_model.HideIfNoPrevNext, ",hideIfNoPrevNext: true");
			stringBuilder.AppendIfTrue(_model.IsRTL, "isRTL: true");
			stringBuilder.AppendFormatIfNotNull(_model.MaxDate, ",minDate: {0}", FormatDateToJavascript(_model.MinDate));
			stringBuilder.AppendFormatIfNotNull(_model.MinDate, ",maxDate: {0}", FormatDateToJavascript(_model.MaxDate));
			stringBuilder.AppendFormatIfTrue(_model.MonthNames.Count > 0, ",monthNames: {0}", JsonConvert.SerializeObject((object)_model.MonthNames));
			stringBuilder.AppendFormatIfTrue(_model.MonthNamesShort.Count > 0, ",monthNamesShort: {0}", JsonConvert.SerializeObject((object)_model.MonthNamesShort));
			stringBuilder.AppendIfTrue(_model.NavigationAsDateFormat, ",navigationAsDateFormat: true");
			stringBuilder.AppendFormatIfTrue(_model.NextText != "Next", ",nextText: '{0}'", _model.NextText);
			stringBuilder.AppendFormatIfTrue(_model.PrevText != "Prev", ",prevText: '{0}'", _model.PrevText);
			stringBuilder.AppendIfTrue(_model.ShowButtonPanel, ",showButtonPanel: true");
			stringBuilder.AppendIfTrue(_model.ShowMonthAfterYear, ",showMonthAfterYear: true");
			stringBuilder.AppendFormat(",showOn: '{0}'", _model.ShowOn.ToString().ToLower());
			return stringBuilder.ToString();
		}

		private string FormatDateToJavascript(DateTime? dateTime)
		{
			if (dateTime.HasValue)
			{
				return $"new Date({dateTime.Value.Year.ToString()},{(dateTime.Value.Month - 1).ToString()},{dateTime.Value.Day.ToString()})";
			}
			return "";
		}

		private string ConvertDotNetDateFormatToJquery(string dateFormat)
		{
			dateFormat = dateFormat.Replace("yy", "y");
			if (dateFormat.IndexOf("MMMM") > 0)
			{
				dateFormat = dateFormat.Replace("MMMM", "MM");
			}
			else if (dateFormat.IndexOf("MMM") > 0)
			{
				dateFormat = dateFormat.Replace("MMM", "M");
			}
			else if (dateFormat.IndexOf("MM") > 0)
			{
				dateFormat = dateFormat.Replace("MM", "mm");
			}
			else if (dateFormat.IndexOf("M") > 0)
			{
				dateFormat = dateFormat.Replace("M", "n");
			}
			if (dateFormat.IndexOf("DDDD") > 0)
			{
				dateFormat = dateFormat.Replace("DDDD", "DD");
			}
			else if (dateFormat.IndexOf("DDD") > 0)
			{
				dateFormat = dateFormat.Replace("DDD", "D");
			}
			else if (dateFormat.IndexOf("DD") > 0)
			{
				dateFormat = dateFormat.Replace("DD", "dd");
			}
			else if (dateFormat.IndexOf("D") > 0)
			{
				dateFormat = dateFormat.Replace("D", "d");
			}
			return dateFormat;
		}
	}
}
