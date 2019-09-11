using System.Collections.Generic;
using System.Text;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal static class CoreGridUtil
	{
		internal static List<string> GetListOfColumns(CoreGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((CoreColumn c) => c.EditType == EditType.DatePicker || c.EditType == EditType.AutoComplete).ForEach(delegate(CoreColumn c)
			{
				Guard.IsNotNullOrEmpty(c.EditorControlID, "CoreGridColumn.EditorControlID", "must be set to the ID of the editing control control if EditType = DatePicker or EditType = AutoComplete");
				result.Add(c.EditType.ToString().ToLower() + ":" + c.DataField);
			});
			return result;
		}

		internal static List<string> GetListOfEditors(CoreGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((CoreColumn c) => c.EditType == EditType.DatePicker || c.EditType == EditType.AutoComplete).ForEach(delegate(CoreColumn c)
			{
				Guard.IsNotNullOrEmpty(c.EditorControlID, "CoreGridColumn.EditorControlID", "must be set to the ID of the editing control control if EditType = DatePicker or EditType = AutoComplete");
				result.Add(c.EditorControlID);
			});
			return result;
		}

		internal static List<string> GetListOfSearchColumns(CoreGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((CoreColumn c) => c.SearchType == SearchType.DatePicker || c.SearchType == SearchType.AutoComplete).ForEach(delegate(CoreColumn c)
			{
				Guard.IsNotNullOrEmpty(c.SearchControlID, "CoreGridColumn.SearchControlID", "must be set to the ID of the searching control control if SearchType = DatePicker or SearchType = AutoComplete");
				result.Add(c.SearchType.ToString().ToLower() + ":" + c.DataField);
			});
			return result;
		}

		internal static List<string> GetListOfSearchEditors(CoreGrid grid)
		{
			List<string> result = new List<string>();
			grid.Columns.FindAll((CoreColumn c) => c.SearchType == SearchType.DatePicker || c.SearchType == SearchType.AutoComplete).ForEach(delegate(CoreColumn c)
			{
				Guard.IsNotNullOrEmpty(c.SearchControlID, "CoreGridColumn.SearchControlID", "must be set to the ID of the searching control control if SearchType = DatePicker or SearchType = AutoComplete");
				result.Add(c.SearchControlID);
			});
			return result;
		}

		internal static string GetAttachEditorsFunction(CoreGrid grid, string editorType, string editorControlID)
		{
			List<string> listOfColumns = GetListOfColumns(grid);
			List<string> listOfEditors = GetListOfEditors(grid);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("function(el) {");
			stringBuilder.Append("setTimeout(function() {");
			stringBuilder.AppendFormat("var ec = '{0}';", editorControlID);
		    if (editorType == "datepickerjquery")
		    {
		        stringBuilder.Append("if (typeof(jQuery(el).datepicker) !== 'function')");
		        stringBuilder.Append("alert('CoreDatePicker javascript not present on the page. Please, include jquery.jqDatePicker.min.js');");
		        stringBuilder.Append("jQuery(el).datepicker( eval(ec + '_dpid') );");
            }
		    if (editorType == "datepicker")
		    {
		        stringBuilder.Append(" $(el).datepicker({");
		        stringBuilder.Append("autoclose: true,");
		        stringBuilder.Append("format: 'dd.mm.yyyy',");
		        stringBuilder.Append("language: 'ru',");
                stringBuilder.Append("weekStart: 1,");
		        stringBuilder.Append("orientation : 'bottom'");
		        stringBuilder.Append(" }).on('changeDate',function(e){setTimeout(function(){if ($(e.currentTarget).attr('searchoperators')=='true') $('#" + grid.ID + "')[0].triggerToolbar();}, 50);});");
		    }
            if (editorType == "autocomplete")
			{
				stringBuilder.Append("if (typeof(jQuery(el).autocomplete) !== 'function')");
				stringBuilder.Append("alert('CoreAutoComplete javascript not present on the page. Please, include jquery.jqAutoComplete.min.js');");
				stringBuilder.Append("jQuery(el).autocomplete( eval(ec + '_acid') );");
			}
			stringBuilder.Append("},200);");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
