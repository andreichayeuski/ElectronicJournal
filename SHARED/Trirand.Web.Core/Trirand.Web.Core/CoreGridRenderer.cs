using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class CoreGridRenderer
	{
		public string RenderHtml(CoreGrid grid)
		{
			string text = "<table id='{0}'></table>";
			if (grid.ToolBarSettings.ToolBarPosition != ToolBarPosition.Hidden)
			{
				text += "<div id='{0}_pager'></div>";
			}
			//if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
			//{
			//	text = "Yes .This is a trial version of CoreGrid for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
			//	return text + File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString();
			//}
			if (string.IsNullOrEmpty(grid.ID))
			{
				throw new Exception("You need to set ID for this grid.");
			}
			text = string.Format(text, grid.ID);
			return (grid.HierarchySettings.HierarchyMode != HierarchyMode.Child && grid.HierarchySettings.HierarchyMode != HierarchyMode.ParentAndChild) ? (text + GetStartupJavascript(grid, subgrid: false)) : (text + GetChildSubGridJavaScript(grid));
		}

		private string GetStartupJavascript(CoreGrid grid, bool subgrid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.Append(GetStartupOptions(grid, subgrid));
			stringBuilder.Append("});");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		private string GetChildSubGridJavaScript(CoreGrid grid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.AppendFormat("function showSubGrid_{0}(subgrid_id, row_id, message, suffix) {{", grid.ID);
			stringBuilder.Append("var subgrid_table_id, pager_id;\r\n\t\t                subgrid_table_id = subgrid_id+'_t';\r\n\t\t                pager_id = 'p_'+ subgrid_table_id;\r\n                        if (suffix) { subgrid_table_id += suffix; pager_id += suffix;  }\r\n                        if (message) jQuery('#'+subgrid_id).append(message);                        \r\n\t\t                jQuery('#'+subgrid_id).append('<table id=' + subgrid_table_id + ' class=scroll></table><div id=' + pager_id + ' class=scroll></div>');\r\n                ");
			stringBuilder.Append(GetStartupOptions(grid, subGrid: true));
			stringBuilder.Append("}");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		private string GetStartupOptions(CoreGrid grid, bool subGrid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = subGrid ? "jQuery('#' + subgrid_table_id)" : $"jQuery('#{grid.ID}')";
			string arg = subGrid ? "jQuery('#' + pager_id)" : string.Format("jQuery('#{0}')", grid.ID + "_pager");
			string pagerSelectorID = subGrid ? "'#' + pager_id" : string.Format("'#{0}'", grid.ID + "_pager");
			string text2 = subGrid ? "&parentRowID=' + row_id + '" : string.Empty;
			string text3 = (grid.DataUrl.IndexOf("?") > 0) ? "&" : "?";
			string text4 = (grid.EditUrl.IndexOf("?") > 0) ? "&" : "?";
			string text5 = $"{grid.DataUrl}{text3}jqGridID={grid.ID}{text2}";
			string arg2 = $"{grid.EditUrl}{text4}jqGridID={grid.ID}&editMode=1{text2}";
			if (grid.Columns.Count > 0 && grid.Columns[0].Frozen)
			{
				grid.AppearanceSettings.ShrinkToFit = false;
			}
			string value = $"{text}.jqGrid({{";
			if (grid.PivotSettings.IsPivotEnabled())
			{
				string arg3 = grid.PivotSettings.ToJSON();
				value = $"{text}.jqGrid('jqPivot','{text5}',{arg3},{{";
			}
			stringBuilder.Append(value);
			stringBuilder.AppendFormat("url: '{0}'", text5);
			stringBuilder.AppendFormat(",editurl: '{0}'", arg2);
			if (!string.IsNullOrEmpty(grid.IDPrefix))
			{
				stringBuilder.AppendFormat(",idPrefix: '{0}'", grid.IDPrefix);
			}
			if (!grid.PivotSettings.IsPivotEnabled())
			{
				stringBuilder.AppendFormat(",datatype: 'json'");
			}
			stringBuilder.AppendFormat(",page: {0}", grid.PagerSettings.CurrentPage);
			if (!grid.PivotSettings.IsPivotEnabled())
			{
				stringBuilder.AppendFormat(",colNames: {0}", GetColNames(grid));
				stringBuilder.AppendFormat(",colModel: {0}", GetColModel(grid));
			}
			stringBuilder.AppendFormat(",viewrecords: true");
			stringBuilder.AppendFormatIfTrue(grid.AutoEncode, ",autoencode: true");
			stringBuilder.AppendFormat(",scrollrows: {0}", grid.ScrollToSelectedRow.ToString().ToLower());
			if (grid.SortSettings.MultiColumnSorting)
			{
				stringBuilder.AppendFormat(",multiSort: true");
			}
			stringBuilder.AppendFormat(",prmNames: {{ id: \"{0}\" }}", Util.GetPrimaryKeyField(grid));
			if (grid.AppearanceSettings.ShowFooter)
			{
				stringBuilder.Append(",footerrow: true");
				stringBuilder.Append(",userDataOnFooter: true");
			}
			if (!grid.AppearanceSettings.ShrinkToFit)
			{
				stringBuilder.Append(",shrinkToFit: false");
			}
			stringBuilder.Append(",headertitles: true");
			if (grid.ColumnReordering)
			{
				stringBuilder.Append(",sortable: true");
			}
		    if (grid.StoreNavigationOptions)
		    {
		        stringBuilder.Append(",storeNavOptions: true");
		    }
            if (grid.AppearanceSettings.ScrollBarOffset != 18)
			{
				stringBuilder.AppendFormat(",scrollOffset: {0}", grid.AppearanceSettings.ScrollBarOffset);
			}
			if (grid.AppearanceSettings.RightToLeft)
			{
				stringBuilder.Append(",direction: 'rtl'");
			}
			if (grid.AutoWidth)
			{
				stringBuilder.Append(",autowidth: true");
			}
			if (!grid.ShrinkToFit)
			{
				stringBuilder.Append(",shrinkToFit: false");
			}
		    if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Top)
		    {
		        stringBuilder.Append(",toppager: true");
		    }
            if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Bottom)
			{
				stringBuilder.AppendFormat(",pager: {0}", arg);
			}
			if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom)
			{
			    stringBuilder.AppendFormat(",pager: {0}", arg);
                stringBuilder.Append(",toppager: true");
			}
			if (grid.RenderingMode == RenderingMode.Optimized)
			{
				if (grid.HierarchySettings.HierarchyMode != 0)
				{
					throw new Exception("Optimized rendering is not compatible with hierarchy.");
				}
				stringBuilder.Append(",gridview: true");
			}
			if (grid.HierarchySettings.HierarchyMode == HierarchyMode.Parent || grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild)
			{
				stringBuilder.Append(",subGrid: true");
				stringBuilder.AppendFormat(",subGridOptions: {0}", grid.HierarchySettings.ToJSON());
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.SubGridRowExpanded))
			{
				stringBuilder.AppendFormat(",subGridRowExpanded: {0}", grid.ClientSideEvents.SubGridRowExpanded);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.ServerError))
			{
				stringBuilder.AppendFormat(",errorCell: {0}", grid.ClientSideEvents.ServerError);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowSelect))
			{
				stringBuilder.AppendFormat(",onSelectRow: {0}", grid.ClientSideEvents.RowSelect);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.ColumnSort))
			{
				stringBuilder.AppendFormat(",onSortCol: {0}", grid.ClientSideEvents.ColumnSort);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowDoubleClick))
			{
				stringBuilder.AppendFormat(",ondblClickRow: {0}", grid.ClientSideEvents.RowDoubleClick);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowRightClick))
			{
				stringBuilder.AppendFormat(",onRightClickRow: {0}", grid.ClientSideEvents.RowRightClick);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.LoadDataError))
			{
				stringBuilder.AppendFormat(",loadError: {0}", grid.ClientSideEvents.LoadDataError);
			}
			else
			{
				stringBuilder.AppendFormat(",loadError: {0}", "jqGrid_aspnet_loadErrorHandler");
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.GridInitialized))
			{
				stringBuilder.AppendFormat(",gridComplete: {0}", grid.ClientSideEvents.GridInitialized);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.BeforeAjaxRequest))
			{
				stringBuilder.AppendFormat(",beforeRequest: {0}", grid.ClientSideEvents.BeforeAjaxRequest);
			}
            if (!string.IsNullOrEmpty(grid.ClientSideEvents.AfterAjaxRequest))
			{
				stringBuilder.AppendFormat(",loadComplete: {0}", grid.ClientSideEvents.AfterAjaxRequest);
			}
			stringBuilder.Append(",colMenu: true");
			if (grid.TreeGridSettings.Enabled)
			{
				stringBuilder.AppendFormat(",treeGrid: true");
				stringBuilder.AppendFormat(",treedatatype: 'json'");
				stringBuilder.AppendFormat(",treeGridModel: 'adjacency'");
				string arg4 = "{ level_field: 'tree_level', parent_id_field: 'tree_parent', leaf_field: 'tree_leaf', expanded_field: 'tree_expanded', loaded: 'tree_loaded', icon_field: 'tree_icon' }";
				stringBuilder.AppendFormat(",treeReader: {0}", arg4);
				stringBuilder.AppendFormat(",ExpandColumn: '{0}'", GetFirstVisibleDataField(grid));
				Hashtable hashtable = new Hashtable();
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.CollapsedIcon))
				{
					hashtable.Add("plus", grid.TreeGridSettings.CollapsedIcon);
				}
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.ExpandedIcon))
				{
					hashtable.Add("minus", grid.TreeGridSettings.ExpandedIcon);
				}
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.LeafIcon))
				{
					hashtable.Add("leaf", grid.TreeGridSettings.LeafIcon);
				}
				if (hashtable.Count > 0)
				{
					stringBuilder.AppendFormat(",treeIcons: {0}", JsonConvert.SerializeObject((object)hashtable));
				}
			}
			if (grid.LoadOnce)
			{
				stringBuilder.Append(",loadonce: true");
				stringBuilder.Append(",ignoreCase: true");
			}
		    if (grid.ColumnMenu)
		    {
		        stringBuilder.Append(",colMenu: true");
		    }
            if (!grid.AppearanceSettings.HighlightRowsOnHover)
			{
				stringBuilder.Append(",hoverrows: false");
			}
			if (grid.AppearanceSettings.AlternateRowBackground)
			{
				stringBuilder.Append(",altRows: true");
			}
			if (grid.AppearanceSettings.ShowRowNumbers)
			{
				stringBuilder.Append(",rownumbers: true");
			}
			if (grid.AppearanceSettings.RowNumbersColumnWidth != 25)
			{
				stringBuilder.AppendFormat(",rownumWidth: {0}", grid.AppearanceSettings.RowNumbersColumnWidth.ToString());
			}
			if (grid.PagerSettings.ScrollBarPaging)
			{
				stringBuilder.AppendFormat(",scroll: 1");
			}
			stringBuilder.AppendFormat(",rowNum: {0}", grid.PagerSettings.PageSize.ToString());
			stringBuilder.AppendFormat(",rowList: {0}", grid.PagerSettings.PageSizeOptions.ToString());
			if (!string.IsNullOrEmpty(grid.PagerSettings.NoRowsMessage))
			{
				stringBuilder.AppendFormat(",emptyrecords: '{0}'", grid.PagerSettings.NoRowsMessage.ToString());
			}
			if (grid.Responsive)
			{
				stringBuilder.Append(",responsive: true");
			}
			if (grid.StyleUI != "jQueryUI")
			{
				stringBuilder.AppendFormat(",styleUI: '{0}'", grid.StyleUI);
			}
			stringBuilder.AppendFormat(",editDialogOptions: {0}", GetEditOptions(grid));
			stringBuilder.AppendFormat(",addDialogOptions: {0}", GetAddOptions(grid));
			stringBuilder.AppendFormat(",delDialogOptions: {0}", GetDelOptions(grid));
			stringBuilder.AppendFormat(",searchDialogOptions: {0}", GetSearchOptions(grid));
			stringBuilder.AppendFormat(",viewRowDialogOptions: {0}", GetViewRowOptions(grid));
			string format = (!grid.TreeGridSettings.Enabled) ? ",jsonReader: {{ repeatitems:false,subgrid:{{repeatitems:false}}  }}" : ",jsonReader: {{ id: \"{0}\", repeatitems:false,subgrid:{{repeatitems:false}} }}";
			if (grid.PivotSettings.IsPivotEnabled())
			{
				stringBuilder.Append(",jsonReader: { repeatitems:false,subgrid:{repeatitems:false} }");
			}
			else
			{
				stringBuilder.AppendFormat(format, grid.Columns[Util.GetPrimaryKeyIndex(grid)].DataField);
			}
			if (!string.IsNullOrEmpty(grid.SortSettings.InitialSortColumn))
			{
				stringBuilder.AppendFormat(",sortname: '{0}'", grid.SortSettings.InitialSortColumn);
			}
			stringBuilder.AppendFormat(",sortorder: '{0}'", grid.SortSettings.InitialSortDirection.ToString().ToLower());
			if (grid.MultiSelect)
			{
				stringBuilder.Append(",multiselect: true");
				if (grid.MultiSelectMode == MultiSelectMode.SelectOnCheckBoxClickOnly)
				{
					stringBuilder.AppendFormat(",multiboxonly: true", grid.MultiSelect.ToString().ToLower());
				}
				if (grid.MultiSelectKey != 0)
				{
					stringBuilder.AppendFormat(",multikey: '{0}'", GetMultiKeyString(grid.MultiSelectKey));
				}
			}
			if (!string.IsNullOrEmpty(grid.AppearanceSettings.Caption))
			{
				stringBuilder.AppendFormat(",caption: '{0}'", grid.AppearanceSettings.Caption);
			}
			if (grid.AppearanceSettings.HiddenGrid)
			{
				stringBuilder.Append(",hiddengrid:true");
			}
			if (!grid.AppearanceSettings.ShowHideGridCaptionButton)
			{
				stringBuilder.Append(",hidegrid:false");
			}
			if (!string.IsNullOrEmpty(grid.Width))
			{
				stringBuilder.AppendFormat(",width: '{0}'", grid.Width.ToString().Replace("px", ""));
			}
			if (!string.IsNullOrEmpty(grid.Height))
			{
				stringBuilder.AppendFormat(",height: '{0}'", grid.Height.ToString().Replace("px", ""));
			}
			if (!string.IsNullOrEmpty(grid.PostData))
			{
				stringBuilder.AppendFormat(",postData: {0}", grid.PostData);
			}
			if (grid.GroupSettings.GroupFields.Count > 0)
			{
				stringBuilder.Append(grid.GroupSettings.ToJSON());
			}
			stringBuilder.AppendFormat(",viewsortcols: [{0},'{1}',{2}]", "false", grid.SortSettings.SortIconsPosition.ToString().ToLower(), (grid.SortSettings.SortAction == SortAction.ClickOnHeader) ? "true" : "false");
			stringBuilder.AppendFormat("}})\r");
			stringBuilder.Append(";");
			stringBuilder.Append(GetLoadErrorHandler());
			stringBuilder.Append(";");
			if (grid.PivotSettings.IsPivotEnabled())
			{
				stringBuilder.AppendFormat("{0}.bind('jqGridInitGrid.pivotGrid',(function(){{", text);
			}
			if (!grid.PagerSettings.ScrollBarPaging && grid.EnableKeyboardNavigation)
			{
				stringBuilder.AppendFormat("{0}.bindKeys();", text);
			}
			stringBuilder.Append(GetToolBarOptions(grid, subGrid, pagerSelectorID, text));
			if (grid.PivotSettings.IsPivotEnabled())
			{
				stringBuilder.Append("}));");
			}
			if (grid.HeaderGroups.Count > 0)
			{
				List<Hashtable> list = new List<Hashtable>();
				foreach (CoreGridHeaderGroup headerGroup in grid.HeaderGroups)
				{
					list.Add(headerGroup.ToHashtable());
				}
				stringBuilder.AppendFormat("{0}.setGroupHeaders( {{ useColSpanStyle:true,groupHeaders:{1} }});", text, JsonConvert.SerializeObject((object)list));
			}
			if (grid.ToolBarSettings.ShowSearchToolBar)
			{
				stringBuilder.AppendFormat("{0}.filterToolbar({1});", text, new JsonSearchToolBar(grid).Process());
			}
			if (grid.Columns.Count > 0 && grid.Columns[0].Frozen)
			{
				stringBuilder.AppendFormat("{0}.setFrozenColumns();", text);
			}
			return PreProcessJSON(grid, stringBuilder.ToString());
		}

		private string PreProcessJSON(CoreGrid grid, string gridJSON)
		{
			foreach (string key in grid.FunctionsHash.Keys)
			{
				string oldValue = $"\"{key}\":\"{grid.FunctionsHash[key]}\"";
				string newValue = $"\"{key}\":{grid.FunctionsHash[key]}";
				gridJSON = gridJSON.Replace(oldValue, newValue);
			}
			foreach (string key2 in grid.ReplacementsHash.Keys)
			{
				gridJSON = gridJSON.Replace(key2, grid.ReplacementsHash[key2].ToString());
			}
			return gridJSON;
		}

		private string GetEditOptions(CoreGrid grid)
		{
			JsonEditDialog jsonEditDialog = new JsonEditDialog(grid);
			return jsonEditDialog.Process();
		}

		private string GetAddOptions(CoreGrid grid)
		{
			JsonAddDialog jsonAddDialog = new JsonAddDialog(grid);
			return jsonAddDialog.Process();
		}

		private string GetDelOptions(CoreGrid grid)
		{
			JsonDelDialog jsonDelDialog = new JsonDelDialog(grid);
			return jsonDelDialog.Process();
		}

		private string GetSearchOptions(CoreGrid grid)
		{
			JsonSearchDialog jsonSearchDialog = new JsonSearchDialog(grid);
			return jsonSearchDialog.Process();
		}

		private string GetViewRowOptions(CoreGrid grid)
		{
			JsonViewRowDialog jsonViewRowDialog = new JsonViewRowDialog(grid);
			return jsonViewRowDialog.Process();
		}

		private string GetColNames(CoreGrid grid)
		{
			string[] array = new string[grid.Columns.Count];
			for (int i = 0; i < grid.Columns.Count; i++)
			{
				CoreColumn coreColumn = grid.Columns[i];
				array[i] = (string.IsNullOrEmpty(coreColumn.HeaderText) ? coreColumn.DataField : coreColumn.HeaderText);
			}
			return JsonConvert.SerializeObject((object)array);
		}

		private string GetColModel(CoreGrid grid)
		{
			Hashtable[] array = new Hashtable[grid.Columns.Count];
			for (int i = 0; i < grid.Columns.Count; i++)
			{
				JsonColModel jsonColModel = new JsonColModel(grid.Columns[i], grid);
				array[i] = jsonColModel.JsonValues;
			}
			string input = JsonConvert.SerializeObject((object)array);
			return JsonColModel.RemoveQuotesForJavaScriptMethods(input, grid);
		}

		private string GetMultiKeyString(MultiSelectKey key)
		{
			switch (key)
			{
			case MultiSelectKey.Alt:
				return "altKey";
			case MultiSelectKey.Shift:
				return "shiftKey";
			case MultiSelectKey.Ctrl:
				return "ctrlKey";
			default:
				throw new Exception("Should not be here.");
			}
		}

		private string GetToolBarOptions(CoreGrid grid, bool subGrid, string pagerSelectorID, string tableSelector)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (grid.ShowToolBar)
			{
				JsonToolBar jsonToolBar = new JsonToolBar(grid.ToolBarSettings);
				if (!subGrid)
				{
					stringBuilder.AppendFormat("{7}.navGrid('#{0}',{1},{2},{3},{4},{5},{6} );", grid.ID + "_pager", JsonConvert.SerializeObject((object)jsonToolBar), $"jQuery('#{grid.ID}').getGridParam('editDialogOptions')", $"jQuery('#{grid.ID}').getGridParam('addDialogOptions')", $"jQuery('#{grid.ID}').getGridParam('delDialogOptions')", $"jQuery('#{grid.ID}').getGridParam('searchDialogOptions')", $"jQuery('#{grid.ID}').getGridParam('viewRowDialogOptions')", tableSelector);
				}
				else
				{
					stringBuilder.AppendFormat("{6}.navGrid('#' + pager_id,{0},{1},{2},{3},{4} );", JsonConvert.SerializeObject((object)jsonToolBar), "jQuery('#' + subgrid_table_id).getGridParam('editDialogOptions')", "jQuery('#' + subgrid_table_id).getGridParam('addDialogOptions')", "jQuery('#' + subgrid_table_id).getGridParam('delDialogOptions')", "jQuery('#' + subgrid_table_id).getGridParam('searchDialogOptions')", "jQuery('#' + subgrid_table_id).getGridParam('viewRowDialogOptions')", tableSelector);
				}
				foreach (CoreGridToolBarButton customButton in grid.ToolBarSettings.CustomButtons)
				{
					if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Bottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom)
					{
						JsonCustomButton jsonCustomButton = new JsonCustomButton(customButton);
						stringBuilder.AppendFormat("{2}.navButtonAdd({0},{1});", pagerSelectorID, jsonCustomButton.Process(), tableSelector);
					}
					if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Top)
					{
						JsonCustomButton jsonCustomButton2 = new JsonCustomButton(customButton);
						stringBuilder.AppendFormat("{2}.navButtonAdd({0},{1});", pagerSelectorID.Replace("_pager", "_toppager"), jsonCustomButton2.Process(), tableSelector);
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		private string GetLoadErrorHandler()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\n");
			stringBuilder.Append("function jqGrid_aspnet_loadErrorHandler(xht, st, handler) {");
			stringBuilder.Append("jQuery(document.body).css('font-size','100%'); jQuery(document.body).html(xht.responseText);");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		private string GetFirstVisibleDataField(CoreGrid grid)
		{
			foreach (CoreColumn column in grid.Columns)
			{
				if (column.Visible)
				{
					return column.DataField;
				}
			}
			return grid.Columns[0].DataField;
		}
	}
}
