using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonColModel
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

		public JsonColModel(CoreGrid grid)
		{
			_jsonValues = new Hashtable();
			_grid = grid;
		}

		public JsonColModel(CoreColumn column, CoreGrid grid)
			: this(grid)
		{
			FromColumn(column);
		}

		public void FromColumn(CoreColumn column)
		{
			object obj2 = _jsonValues["index"] = (_jsonValues["name"] = column.DataField);
			if (column.Width != 150)
			{
				_jsonValues["width"] = column.Width;
			}
			if (!column.Sortable)
			{
				_jsonValues["sortable"] = false;
			}
			if (column.PrimaryKey)
			{
				_jsonValues["key"] = true;
			}


            if (!column.Visible)
			{
				_jsonValues["hidden"] = true;
			}
			if (!column.Searchable)
			{
				_jsonValues["search"] = false;
			}
			if (column.TextAlign != 0)
			{
				_jsonValues["align"] = column.TextAlign.ToString().ToLower();
			}
			if (!column.Resizable)
			{
				_jsonValues["resizable"] = false;
			}
			if (column.Frozen)
			{
				_jsonValues["frozen"] = true;
			}
			if (!string.IsNullOrEmpty(column.CssClass))
			{
				_jsonValues["classes"] = column.CssClass;
			}
			if (column.Fixed)
			{
				_jsonValues["fixed"] = true;
			}
			if (column.ShowColumnMenu)
			{
				_jsonValues["colmenu"] = true;

			    if (column.ColumnMenuOptions != null)
			    {
			        Hashtable hashtable = new Hashtable();
			        hashtable["sorting"] = column.ColumnMenuOptions.Sorting;
			        hashtable["columns"] = column.ColumnMenuOptions.Columns;
			        hashtable["filtering"] = column.ColumnMenuOptions.Filtering;
			        hashtable["seraching"] = column.ColumnMenuOptions.Searching;
			        hashtable["grouping"] = column.ColumnMenuOptions.Grouping;
			        hashtable["freeze"] = column.ColumnMenuOptions.Freezing;
			        this._jsonValues["coloptions"] = hashtable;
			    }

            }
			else
			{
				_jsonValues["colmenu"] = false;
			}
			if (column.GroupSummaryType != 0)
			{
				switch (column.GroupSummaryType)
				{
				case GroupSummaryType.Avg:
					_jsonValues["summaryType"] = "avg";
					break;
				case GroupSummaryType.Count:
					_jsonValues["summaryType"] = "count";
					break;
				case GroupSummaryType.Max:
					_jsonValues["summaryType"] = "max";
					break;
				case GroupSummaryType.Min:
					_jsonValues["summaryType"] = "min";
					break;
				case GroupSummaryType.Sum:
					_jsonValues["summaryType"] = "sum";
					break;
				}
			}
			if (!string.IsNullOrEmpty(column.GroupTemplate))
			{
				_jsonValues["summaryTpl"] = column.GroupTemplate;
			}
			if (column.Formatter != null || column.EditActionIconsColumn)
			{
				ApplyFormatterOptions(column);
			}
			if (column.EditActionIconsColumn)
			{
				_jsonValues["formatter"] = "actions";
			}
			if (_grid.TreeGridSettings.Enabled && column.DataType != null)
			{
				if (column.DataType == typeof(string))
				{
					_jsonValues["sorttype"] = "string";
				}
				if (column.DataType == typeof(int))
				{
					_jsonValues["sorttype"] = "int";
				}
				if (column.DataType == typeof(float) || column.DataType == typeof(decimal))
				{
					_jsonValues["sorttype"] = "float";
				}
				if (column.DataType == typeof(DateTime))
				{
					_jsonValues["sorttype"] = "date";
				}
			}
			if (column.Searchable)
			{
				Hashtable hashtable = new Hashtable();
				if (column.SearchType == SearchType.DropDown)
				{
					_jsonValues["stype"] = "select";
				}
				if (!column.Visible)
				{
					hashtable["searchhidden"] = true;
				}
				if (column.SearchList.Count() > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					int num = 0;
					foreach (SelectListItem search in column.SearchList)
					{
						stringBuilder.AppendFormat("{0}:{1}", search.Value, search.Text);
						num++;
						if (num < column.SearchList.Count())
						{
							stringBuilder.Append(";");
						}
						if (search.Selected)
						{
							hashtable["defaultValue"] = search.Value;
						}
					}
					hashtable["value"] = stringBuilder.ToString();
				}
				if (column.SearchType == SearchType.DatePicker || column.SearchType == SearchType.AutoComplete)
				{
					hashtable["dataInit"] = "attachSearchControlsScript" + column.DataField;
				}
				if (column.SearchOptions.Count > 0)
				{
					hashtable["sopt"] = GetSearchOptionsArray(column.SearchOptions);
				}
				if (!column.ClearSearch)
				{
					hashtable["clearSearch"] = false;
				}

			    hashtable["searchOperators"] = column.ShowSearchOperators;

                _jsonValues["searchoptions"] = hashtable;
			}
			if (column.Editable)
			{
				Hashtable hashtable2 = new Hashtable();
				_jsonValues["editable"] = true;
				if (column.EditType == EditType.CheckBox)
				{
					hashtable2["value"] = "True:False";
				}
				if (column.EditType != EditType.TextBox)
				{
					_jsonValues["edittype"] = GetEditType(column.EditType);
				}
				if (column.EditType == EditType.Custom)
				{
					Guard.IsNotNullOrEmpty(column.EditTypeCustomCreateElement, "CoreGridColumn.EditTypeCustomCreateElement", " should be set to the name of the javascript function creating the element when EditType = EditType.Custom");
					Guard.IsNotNullOrEmpty(column.EditTypeCustomGetValue, "CoreGridColumn.EditTypeCustomGetValue", " should be set to the name of the javascript function getting the value from the element when EditType = EditType.Custom");
					hashtable2["custom_element"] = column.EditTypeCustomCreateElement;
					hashtable2["custom_value"] = column.EditTypeCustomGetValue;
				}
				foreach (CoreGridEditFieldAttribute editFieldAttribute in column.EditFieldAttributes)
				{
					hashtable2[editFieldAttribute.Name] = editFieldAttribute.Value;
				}
				if (column.EditType == EditType.DatePicker || column.EditType == EditType.AutoComplete)
				{
					hashtable2["dataInit"] = "attachEditControlsScript" + column.DataField;
				}
				if (column.EditList.Count > 0)
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					int num2 = 0;
					foreach (SelectListItem edit in column.EditList)
					{
						stringBuilder2.AppendFormat("{0}:{1}", edit.Value, edit.Text);
						num2++;
						if (num2 < column.EditList.Count())
						{
							stringBuilder2.Append(";");
						}
					}
					hashtable2["value"] = stringBuilder2.ToString();
				}
				if (hashtable2.Count > 0)
				{
					_jsonValues["editoptions"] = hashtable2;
				}
				Hashtable hashtable3 = new Hashtable();
				if (column.EditDialogColumnPosition != 0)
				{
					hashtable3["colpos"] = column.EditDialogColumnPosition;
				}
				if (column.EditDialogRowPosition != 0)
				{
					hashtable3["rowpos"] = column.EditDialogRowPosition;
				}
				if (!string.IsNullOrEmpty(column.EditDialogLabel))
				{
					hashtable3["label"] = column.EditDialogLabel;
				}
				if (!string.IsNullOrEmpty(column.EditDialogFieldPrefix))
				{
					hashtable3["elmprefix"] = column.EditDialogFieldPrefix;
				}
				if (!string.IsNullOrEmpty(column.EditDialogFieldSuffix))
				{
					hashtable3["elmsuffix"] = column.EditDialogFieldSuffix;
				}
				if (hashtable3.Count > 0)
				{
					_jsonValues["formoptions"] = hashtable3;
				}
				Hashtable hashtable4 = new Hashtable();
				if (!column.Visible && column.Editable)
				{
					hashtable4["edithidden"] = true;
				}
				if (column.EditClientSideValidators != null)
				{
					foreach (CoreGridEditClientSideValidator editClientSideValidator in column.EditClientSideValidators)
					{
						if (editClientSideValidator is DateValidator)
						{
							hashtable4["date"] = true;
						}
						if (editClientSideValidator is EmailValidator)
						{
							hashtable4["email"] = true;
						}
						if (editClientSideValidator is IntegerValidator)
						{
							hashtable4["integer"] = true;
						}
						if (editClientSideValidator is MaxValueValidator)
						{
							hashtable4["maxValue"] = ((MaxValueValidator)editClientSideValidator).MaxValue;
						}
						if (editClientSideValidator is MinValueValidator)
						{
							hashtable4["minValue"] = ((MinValueValidator)editClientSideValidator).MinValue;
						}
						if (editClientSideValidator is NumberValidator)
						{
							hashtable4["number"] = true;
						}
						if (editClientSideValidator is RequiredValidator)
						{
							hashtable4["required"] = true;
						}
						if (editClientSideValidator is TimeValidator)
						{
							hashtable4["time"] = true;
						}
						if (editClientSideValidator is UrlValidator)
						{
							hashtable4["url"] = true;
						}
						if (editClientSideValidator is CustomValidator)
						{
							hashtable4["custom"] = true;
							hashtable4["custom_func"] = ((CustomValidator)editClientSideValidator).ValidationFunction;
						}
					}
				}
				if (hashtable4.Count > 0)
				{
					_jsonValues["editrules"] = hashtable4;
				}
			}
		}

		private void ApplyFormatterOptions(CoreColumn column)
		{
			Hashtable hashtable = new Hashtable();
			if (column.EditActionIconsColumn)
			{
				hashtable["keys"] = column.EditActionIconsSettings.SaveOnEnterKeyPress;
				hashtable["editbutton"] = column.EditActionIconsSettings.ShowEditIcon;
				hashtable["delbutton"] = column.EditActionIconsSettings.ShowDeleteIcon;
			}
			if (column.Formatter != null)
			{
				CoreGridColumnFormatter formatter = column.Formatter;
				if (formatter is LinkFormatter)
				{
					LinkFormatter linkFormatter = (LinkFormatter)formatter;
					_jsonValues["formatter"] = "link";
					if (!string.IsNullOrEmpty(linkFormatter.Target))
					{
						hashtable["target"] = linkFormatter.Target;
					}
				}
				if (formatter is EmailFormatter)
				{
					_jsonValues["formatter"] = "email";
				}
				if (formatter is IntegerFormatter)
				{
					IntegerFormatter integerFormatter = (IntegerFormatter)formatter;
					_jsonValues["formatter"] = "integer";
					if (!string.IsNullOrEmpty(integerFormatter.ThousandsSeparator))
					{
						hashtable["thousandsSeparator"] = integerFormatter.ThousandsSeparator;
					}
					if (!string.IsNullOrEmpty(integerFormatter.DefaultValue))
					{
						hashtable["defaultValue"] = integerFormatter.DefaultValue;
					}
				}
				if (formatter is NumberFormatter)
				{
					NumberFormatter numberFormatter = (NumberFormatter)formatter;
					_jsonValues["formatter"] = "integer";
					if (!string.IsNullOrEmpty(numberFormatter.ThousandsSeparator))
					{
						hashtable["thousandsSeparator"] = numberFormatter.ThousandsSeparator;
					}
					if (!string.IsNullOrEmpty(numberFormatter.DefaultValue))
					{
						hashtable["defaultValue"] = numberFormatter.DefaultValue;
					}
					if (!string.IsNullOrEmpty(numberFormatter.DecimalSeparator))
					{
						hashtable["decimalSeparator"] = numberFormatter.DecimalSeparator;
					}
					if (numberFormatter.DecimalPlaces != -1)
					{
						hashtable["decimalPlaces"] = numberFormatter.DecimalPlaces;
					}
				}
				if (formatter is CurrencyFormatter)
				{
					CurrencyFormatter currencyFormatter = (CurrencyFormatter)formatter;
					_jsonValues["formatter"] = "currency";
					if (!string.IsNullOrEmpty(currencyFormatter.ThousandsSeparator))
					{
						hashtable["thousandsSeparator"] = currencyFormatter.ThousandsSeparator;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.DefaultValue))
					{
						hashtable["defaultValue"] = currencyFormatter.DefaultValue;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.DecimalSeparator))
					{
						hashtable["decimalSeparator"] = currencyFormatter.DecimalSeparator;
					}
					if (currencyFormatter.DecimalPlaces != -1)
					{
						hashtable["decimalPlaces"] = currencyFormatter.DecimalPlaces;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.Prefix))
					{
						hashtable["prefix"] = currencyFormatter.Prefix;
					}
					if (!string.IsNullOrEmpty(currencyFormatter.Prefix))
					{
						hashtable["suffix"] = currencyFormatter.Suffix;
					}
				}
				if (formatter is CheckBoxFormatter)
				{
					CheckBoxFormatter checkBoxFormatter = (CheckBoxFormatter)formatter;
					_jsonValues["formatter"] = "checkbox";
					if (checkBoxFormatter.Enabled)
					{
						hashtable["disabled"] = false;
					}
				}
				if (formatter is CustomFormatter)
				{
					CustomFormatter customFormatter = (CustomFormatter)formatter;
					if (!string.IsNullOrEmpty(customFormatter.FormatFunction))
					{
						_jsonValues["formatter"] = customFormatter.FormatFunction;
					}
					if (!string.IsNullOrEmpty(customFormatter.UnFormatFunction))
					{
						_jsonValues["unformat"] = customFormatter.UnFormatFunction;
					}
					if (!string.IsNullOrEmpty(customFormatter.SetAttributesFunction))
					{
						_jsonValues["cellattr"] = customFormatter.SetAttributesFunction;
					}
				}
			}
			if (hashtable.Count > 0)
			{
				_jsonValues["formatoptions"] = hashtable;
			}
		}

		public static string RemoveQuotesForJavaScriptMethods(string input, CoreGrid grid)
		{
			string text = input;
			foreach (CoreColumn column in grid.Columns)
			{
				if (column.Formatter != null)
				{
					CoreGridColumnFormatter formatter = column.Formatter;
					if (formatter is CustomFormatter)
					{
						CustomFormatter customFormatter = (CustomFormatter)formatter;
						string oldValue = $"\"formatter\":\"{customFormatter.FormatFunction}\"";
						string newValue = $"\"formatter\":{customFormatter.FormatFunction}";
						text = text.Replace(oldValue, newValue);
						oldValue = $"\"unformat\":\"{customFormatter.UnFormatFunction}\"";
						newValue = $"\"unformat\":{customFormatter.UnFormatFunction}";
						text = text.Replace(oldValue, newValue);
						oldValue = $"\"cellattr\":\"{customFormatter.SetAttributesFunction}\"";
						newValue = $"\"cellattr\":{customFormatter.SetAttributesFunction}";
						text = text.Replace(oldValue, newValue);
					}
				}
				foreach (CoreGridEditClientSideValidator editClientSideValidator in column.EditClientSideValidators)
				{
					if (editClientSideValidator is CustomValidator)
					{
						CustomValidator customValidator = (CustomValidator)editClientSideValidator;
						string oldValue2 = $"\"custom_func\":\"{customValidator.ValidationFunction}\"";
						string newValue2 = $"\"custom_func\":{customValidator.ValidationFunction}";
						text = text.Replace(oldValue2, newValue2);
					}
				}
				if (column.EditType == EditType.Custom)
				{
					string oldValue3 = $"\"custom_element\":\"{column.EditTypeCustomCreateElement}\"";
					string newValue3 = $"\"custom_element\":{column.EditTypeCustomCreateElement}";
					text = text.Replace(oldValue3, newValue3);
					oldValue3 = $"\"custom_value\":\"{column.EditTypeCustomGetValue}\"";
					newValue3 = $"\"custom_value\":{column.EditTypeCustomGetValue}";
					text = text.Replace(oldValue3, newValue3);
				}
				if ((column.Editable && column.EditType == EditType.DatePicker) || column.EditType == EditType.AutoComplete)
				{
					string attachEditorsFunction = CoreGridUtil.GetAttachEditorsFunction(grid, column.EditType.ToString().ToLower(), column.EditorControlID);
					text = text.Replace("\"attachEditControlsScript" + column.DataField + "\"", attachEditorsFunction);
					text = text.Replace("\"dataInit\"", "dataInit");
				}
				if ((column.Searchable && column.SearchType == SearchType.DatePicker) || column.SearchType == SearchType.AutoComplete)
				{
					string attachEditorsFunction2 = CoreGridUtil.GetAttachEditorsFunction(grid, column.SearchType.ToString().ToLower(), column.SearchControlID);
					text = text.Replace("\"attachSearchControlsScript" + column.DataField + "\"", attachEditorsFunction2);
					text = text.Replace("\"dataInit\"", "dataInit");
				}
			}
			return text;
		}

		private ArrayList GetSearchOptionsArray(List<SearchOperation> options)
		{
			ArrayList arrayList = new ArrayList();
			foreach (SearchOperation option in options)
			{
				arrayList.Add(GetStringFromSearchOperation(option));
			}
			return arrayList;
		}

		public string GetStringFromSearchOperation(SearchOperation operation)
		{
			switch (operation)
			{
			case SearchOperation.IsEqualTo:
				return "eq";
			case SearchOperation.IsNotEqualTo:
				return "ne";
			case SearchOperation.IsLessThan:
				return "lt";
			case SearchOperation.IsLessOrEqualTo:
				return "le";
			case SearchOperation.IsGreaterThan:
				return "gt";
			case SearchOperation.IsGreaterOrEqualTo:
				return "ge";
			case SearchOperation.IsIn:
				return "in";
			case SearchOperation.IsNotIn:
				return "ni";
			case SearchOperation.BeginsWith:
				return "bw";
			case SearchOperation.DoesNotBeginWith:
				return "bn";
			case SearchOperation.EndsWith:
				return "ew";
			case SearchOperation.DoesNotEndWith:
				return "en";
			case SearchOperation.Contains:
				return "cn";
			case SearchOperation.DoesNotContain:
				return "nc";
			default:
				return "eq";
			}
		}

		private string GetEditType(EditType type)
		{
			switch (type)
			{
			case EditType.Password:
				return "password";
			case EditType.DropDown:
				return "select";
			case EditType.TextArea:
				return "textarea";
			case EditType.CheckBox:
				return "checkbox";
			case EditType.TextBox:
				return "text";
			case EditType.Custom:
				return "custom";
			default:
				return "text";
			}
		}
	}
}
