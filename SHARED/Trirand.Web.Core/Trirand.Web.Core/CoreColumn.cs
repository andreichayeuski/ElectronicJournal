using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreColumn
	{
		public bool ShowColumnMenu
		{
			get;
			set;
		}

		public bool Fixed
		{
			get;
			set;
		}

		public CoreGridColumnFormatter Formatter
		{
			get;
			set;
		}

		public List<CoreGridEditClientSideValidator> EditClientSideValidators
		{
			get;
			set;
		}

		public List<CoreGridEditFieldAttribute> EditFieldAttributes
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public bool Sortable
		{
			get;
			set;
		}

		public bool Frozen
		{
			get;
			set;
		}

		public bool Resizable
		{
			get;
			set;
		}

		public bool Editable
		{
			get;
			set;
		}

		public bool PrimaryKey
		{
			get;
			set;
		}

		public EditType EditType
		{
			get;
			set;
		}

		public string EditTypeCustomCreateElement
		{
			get;
			set;
		}

		public string EditTypeCustomGetValue
		{
			get;
			set;
		}

		public string EditorControlID
		{
			get;
			set;
		}

		public List<SelectListItem> EditList
		{
			get;
			set;
		}

		public SearchType SearchType
		{
			get;
			set;
		}

		public List<SearchOperation> SearchOptions
		{
			get;
			set;
		}

		public string SearchControlID
		{
			get;
			set;
		}

		public Type DataType
		{
			get;
			set;
		}

		public List<SelectListItem> SearchList
		{
			get;
			set;
		}

		public bool SearchCaseSensitive
		{
			get;
			set;
		}

		public int EditDialogColumnPosition
		{
			get;
			set;
		}

		public int EditDialogRowPosition
		{
			get;
			set;
		}

		public string EditDialogLabel
		{
			get;
			set;
		}

		public string EditDialogFieldPrefix
		{
			get;
			set;
		}

		public string EditDialogFieldSuffix
		{
			get;
			set;
		}

		public bool EditActionIconsColumn
		{
			get;
			set;
		}

		public EditActionIconsSettings EditActionIconsSettings
		{
			get;
			set;
		}

		public string DataField
		{
			get;
			set;
		}

		public string DataFormatString
		{
			get;
			set;
		}

		public string HeaderText
		{
			get;
			set;
		}

		public TextAlign TextAlign
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}

		public bool Searchable
		{
			get;
			set;
		}

		public bool HtmlEncode
		{
			get;
			set;
		}

		public bool HtmlEncodeFormatString
		{
			get;
			set;
		}

		public bool ConvertEmptyStringToNull
		{
			get;
			set;
		}

		public string NullDisplayText
		{
			get;
			set;
		}

		public SearchOperation SearchToolBarOperation
		{
			get;
			set;
		}

		public string FooterValue
		{
			get;
			set;
		}

		public string CssClass
		{
			get;
			set;
		}

		public GroupSummaryType GroupSummaryType
		{
			get;
			set;
		}

		public string GroupTemplate
		{
			get;
			set;
		}

		public bool ClearSearch
		{
			get;
			set;
		}

        /// <summary>
        /// ширина ячейки при экспорте
        /// </summary>
        public int ExcelWidthRowPx { get; set; }

        public bool ExportToExcel { get; set; } //добавлено из старой версии

	    public bool ShowSearchOperators { get; set; } //добавлено из старой версии

	    /// <summary>
	    /// если нужно использовать какую-то интересную сортировку то нужно создать отдельную невидемую колонку с необходимыми значения, и дальше использовать её имя в качестве этой колонки
	    /// </summary>
	    public string SortByColumnNameValues { get; set; } //добавлено из старой версии

	    public ColumnMenuOptions ColumnMenuOptions { get; set; } //добавлено из старой версии

        //public string TemplateRendererViewName { get; set; } //добавлено из старой версии

	    public string SearchDataField { get; set; } //добавлено из старой версии

        public CoreColumn()
		{
			EditClientSideValidators = new List<CoreGridEditClientSideValidator>();
			EditFieldAttributes = new List<CoreGridEditFieldAttribute>();
			Width = 150;
			Sortable = true;
			Frozen = false;
			Resizable = true;
			Editable = false;
			PrimaryKey = false;
			EditType = EditType.TextBox;
			EditList = new List<SelectListItem>();
			EditTypeCustomCreateElement = "";
			EditTypeCustomGetValue = "";
			SearchType = SearchType.TextBox;
			SearchControlID = "";
			SearchToolBarOperation = SearchOperation.Contains;
			SearchList = new List<SelectListItem>();
			SearchCaseSensitive = false;
			EditDialogColumnPosition = 0;
			EditDialogRowPosition = 0;
			EditDialogLabel = "";
			EditDialogFieldPrefix = "";
			EditDialogFieldSuffix = "";
			EditActionIconsColumn = false;
			EditActionIconsSettings = new EditActionIconsSettings();
			EditorControlID = "";
			DataField = "";
			DataFormatString = "";
			HeaderText = "";
			TextAlign = TextAlign.Left;
			Visible = true;
			Searchable = true;
			HtmlEncode = true;
			HtmlEncodeFormatString = true;
			ConvertEmptyStringToNull = true;
			NullDisplayText = "";
			FooterValue = "";
			CssClass = "";
			GroupSummaryType = GroupSummaryType.None;
			GroupTemplate = "";
			Fixed = false;
			SearchOptions = new List<SearchOperation>();
			ClearSearch = true;
			ShowColumnMenu = false;
		}

		public virtual string FormatDataValue(object dataValue, bool encode)
		{
			if (dataValue != null)
			{
				string text = dataValue.ToString();
				string dataFormatString = DataFormatString;
				int length = text.Length;
				if (!HtmlEncodeFormatString)
				{
					if (length > 0 && encode)
					{
						text = HtmlEncoder.Default.Encode(text);
					}
					if (length == 0 && ConvertEmptyStringToNull)
					{
						return NullDisplayText;
					}
					if (dataFormatString.Length == 0)
					{
						return text;
					}
					if (encode)
					{
						return string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[1]
						{
							text
						});
					}
					return string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[1]
					{
						dataValue
					});
				}
				if (length == 0 && ConvertEmptyStringToNull)
				{
					return NullDisplayText;
				}
				if (!string.IsNullOrEmpty(dataFormatString))
				{
					text = string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[1]
					{
						dataValue
					});
				}
				if (!string.IsNullOrEmpty(text) && encode)
				{
					text = HtmlEncoder.Default.Encode(text);
				}
				return text;
			}
			return NullDisplayText;
		}
	}
}
