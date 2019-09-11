using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trirand.Web.Core.Trirand.Web.Export;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreGrid
	{
		public CoreGridDataResolvedDelegate DataResolved;

		private HttpContext Context
		{
			get;
			set;
		}

		public bool AutoEncode
		{
			get;
			set;
		}

		public bool Responsive
		{
			get;
			set;
		}

		public string StyleUI
		{
			get;
			set;
		}

		public bool AutoWidth
		{
			get;
			set;
		}

		public bool ShrinkToFit
		{
			get;
			set;
		}

		public bool LoadOnce
		{
			get;
			set;
		}

		public bool EnableKeyboardNavigation
		{
			get;
			set;
		}

		public bool ScrollToSelectedRow
		{
			get;
			set;
		}

		public List<CoreColumn> Columns
		{
			get;
			set;
		}

		public List<CoreGridHeaderGroup> HeaderGroups
		{
			get;
			set;
		}

		public EditDialogSettings EditDialogSettings
		{
			get;
			set;
		}

		public AddDialogSettings AddDialogSettings
		{
			get;
			set;
		}

		public DeleteDialogSettings DeleteDialogSettings
		{
			get;
			set;
		}

		public SearchDialogSettings SearchDialogSettings
		{
			get;
			set;
		}

		public ViewRowDialogSettings ViewRowDialogSettings
		{
			get;
			set;
		}

		public SearchToolBarSettings SearchToolBarSettings
		{
			get;
			set;
		}

		public PagerSettings PagerSettings
		{
			get;
			set;
		}

		public ToolBarSettings ToolBarSettings
		{
			get;
			set;
		}

		public SortSettings SortSettings
		{
			get;
			set;
		}

		public AppearanceSettings AppearanceSettings
		{
			get;
			set;
		}

		public HierarchySettings HierarchySettings
		{
			get;
			set;
		}

		public GroupSettings GroupSettings
		{
			get;
			set;
		}

		public TreeGridSettings TreeGridSettings
		{
			get;
			set;
		}

		public GridExportSettings ExportSettings
		{
			get;
			set;
		}

		public PivotSettings PivotSettings
		{
			get;
			set;
		}

		public ClientSideEvents ClientSideEvents
		{
			get;
			set;
		}

		public string ID
		{
			get;
			set;
		}

		public string IDPrefix
		{
			get;
			set;
		}

		public string DataUrl
		{
			get;
			set;
		}

		public string EditUrl
		{
			get;
			set;
		}

		public bool ColumnReordering
		{
			get;
			set;
		}

        public bool StoreNavigationOptions { get; set; } //добавлено из старой версии

		public RenderingMode RenderingMode
		{
			get;
			set;
		}

		public bool MultiSelect
		{
			get;
			set;
		}

		public MultiSelectMode MultiSelectMode
		{
			get;
			set;
		}

		public MultiSelectKey MultiSelectKey
		{
			get;
			set;
		}

		public string Width
		{
			get;
			set;
		}

		public string Height
		{
			get;
			set;
		}

		public object DataSource
		{
			get;
			set;
		}

		public string PostData
		{
			get;
			set;
		}

		internal Hashtable FunctionsHash
		{
			get;
			set;
		}

		internal Hashtable ReplacementsHash
		{
			get;
			set;
		}

        public IExcelRenderSettingsRule ExcelRenderSettingsRule
        {
            get;
            set;
        }

        internal bool ShowToolBar => ToolBarSettings.ShowAddButton || ToolBarSettings.ShowDeleteButton || ToolBarSettings.ShowEditButton || ToolBarSettings.ShowRefreshButton || ToolBarSettings.ShowSearchButton || ToolBarSettings.ShowViewRowDetailsButton || ToolBarSettings.CustomButtons.Count > 0;

		public AjaxCallBackMode AjaxCallBackMode
		{
			get
			{
				//IL_0037: Unknown result type (might be due to invalid IL or missing references)
				//IL_004d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0064: Unknown result type (might be due to invalid IL or missing references)
				AjaxCallBackMode result = AjaxCallBackMode.RequestData;
				if (Context != null)
				{
					HttpRequest request = Context.Request;
					request.ContentType="application/x-www-form-urlencoded";
					string text = request.Form["oper"];
					string value = request.Query["editMode"];
					string value2 = request.Query["_search"];
					if (!string.IsNullOrEmpty(text))
					{
						switch (text)
						{
						case "add":
							return AjaxCallBackMode.AddRow;
						case "edit":
							return AjaxCallBackMode.EditRow;
						case "del":
							return AjaxCallBackMode.DeleteRow;
						}
					}
					if (!string.IsNullOrEmpty(value))
					{
						result = AjaxCallBackMode.EditRow;
					}
					if (!string.IsNullOrEmpty(value2) && Convert.ToBoolean(value2))
					{
						result = AjaxCallBackMode.Search;
					}
				}
				return result;
			}
		}

	    public bool ColumnMenu { get; set; } //добавлено из старой версии

        public CoreGrid(HttpContext context)
		{
			Context = context;
			AutoEncode = false;
			AutoWidth = false;
			ShrinkToFit = true;
			LoadOnce = false;
			ScrollToSelectedRow = false;
			EnableKeyboardNavigation = true;
			EditDialogSettings = new EditDialogSettings();
			AddDialogSettings = new AddDialogSettings();
			DeleteDialogSettings = new DeleteDialogSettings();
			SearchDialogSettings = new SearchDialogSettings();
			SearchToolBarSettings = new SearchToolBarSettings();
			ViewRowDialogSettings = new ViewRowDialogSettings();
			PagerSettings = new PagerSettings();
			ToolBarSettings = new ToolBarSettings();
			SortSettings = new SortSettings();
			AppearanceSettings = new AppearanceSettings();
			HierarchySettings = new HierarchySettings();
			GroupSettings = new GroupSettings();
			TreeGridSettings = new TreeGridSettings();
			ExportSettings = new GridExportSettings();
			ClientSideEvents = new ClientSideEvents();
			PivotSettings = new PivotSettings();
			Columns = new List<CoreColumn>();
			HeaderGroups = new List<CoreGridHeaderGroup>();
			DataUrl = "";
			EditUrl = "";
			ColumnReordering = false;
			RenderingMode = RenderingMode.Default;
			MultiSelect = false;
			MultiSelectMode = MultiSelectMode.SelectOnRowClick;
			MultiSelectKey = MultiSelectKey.None;
			Width = "";
			Height = "";
			ID = "";
			IDPrefix = "";
			PostData = "";
			Responsive = false;
			StyleUI = "jQueryUI";
			FunctionsHash = new Hashtable();
			ReplacementsHash = new Hashtable();
		}

		public JsonResult DataBind(object dataSource)
		{
			DataSource = dataSource;
			return DataBind();
		}

		public JsonResult DataBind()
		{
			switch (AjaxCallBackMode)
			{
			default:
				return GetJsonResponse();
			}
		}

		public ActionResult ShowEditValidationMessage(string errorMessage)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			ContentResult val = new ContentResult();
			val.Content=errorMessage;
			return val;
		}

		private JsonResult FilterDataSource(object dataSource, Dictionary<string, string> queryString, out IQueryable iqueryable)
		{
			//IL_027b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0282: Expected O, but got Unknown
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string searchString = "";
			string searchOper = "";
			string text5 = "";
			string sortDirection = "";
			int num = 1;
			int num2 = 0;
			iqueryable = (dataSource as IQueryable);
			Guard.IsNotNull(iqueryable, "DataSource", "should implement the IQueryable interface.");
			if (queryString.ContainsKey("page"))
			{
				num = Convert.ToInt32(queryString["page"]);
			}
			if (queryString.ContainsKey("rows"))
			{
				num2 = Convert.ToInt32(queryString["rows"]);
			}
			if (queryString.ContainsKey("sidx"))
			{
				text5 = queryString["sidx"];
			}
			if (queryString.ContainsKey("sord"))
			{
				sortDirection = queryString["sord"];
			}
			if (queryString.ContainsKey("parentRowID"))
			{
				text = queryString["parentRowID"];
			}
			if (queryString.ContainsKey("_search"))
			{
				text2 = queryString["_search"];
			}
			if (queryString.ContainsKey("filters"))
			{
				text3 = queryString["filters"];
			}
			if (queryString.ContainsKey("searchField"))
			{
				text4 = queryString["searchField"];
			}
			if (queryString.ContainsKey("searchString"))
			{
				searchString = queryString["searchString"];
			}
			if (queryString.ContainsKey("searchOper"))
			{
				searchOper = queryString["searchOper"];
			}
			PagerSettings.CurrentPage = num;
			if (num2 > 0)
			{
				PagerSettings.PageSize = num2;
			}
			if ((!string.IsNullOrEmpty(text2) && text2 != "false") || !string.IsNullOrEmpty(text3))
			{
				try
				{
					if (string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
					{
						iqueryable = iqueryable.Where(Util.GetWhereClause(this, text4, searchString, searchOper));
					}
					else if (!string.IsNullOrEmpty(text3))
					{
					    var predicate = Util.GetWhereClause(this, text3);
					    if (!string.IsNullOrEmpty(predicate))
					    {
					        iqueryable = iqueryable.Where(predicate);
					    }
					}
					else if (ToolBarSettings.ShowSearchToolBar || text2 == "true")
					{
						iqueryable = iqueryable.Where(Util.GetWhereClause(this, queryString));
					}
				}
				catch (DataTypeNotSetException ex)
				{
					throw ex;
				}
				catch (Exception e)
				{
				    throw e;
                }
			}
			int num3 = iqueryable.Count();
			if (PivotSettings.IsPivotEnabled())
			{
				num2 = num3;
			}
			int totalPagesCount = (int)Math.Ceiling((double)((float)num3 / (float)num2));
			if (string.IsNullOrEmpty(text5) && SortSettings.AutoSortByPrimaryKey)
			{
				if (Columns.Count == 0)
				{
					throw new Exception("CoreGrid must have at least one column defined.");
				}
				text5 = Util.GetPrimaryKeyField(this);
				sortDirection = "asc";
			}
			if (!string.IsNullOrEmpty(text5))
			{
				iqueryable = iqueryable.OrderBy(GetSortExpression(text5, sortDirection));
			}
			if (!LoadOnce && !PivotSettings.IsPivotEnabled())
			{
				iqueryable = iqueryable.Skip((num - 1) * num2).Take(num2);
			}
			List<Hashtable> list = Util.ToListOfHashtables(iqueryable, this);
			DataResolved?.Invoke(new CoreGridDataResolvedEventArgs(this, iqueryable, DataSource as IQueryable));
			JsonTreeResponse response = new JsonTreeResponse(num, totalPagesCount, num3, num2, list.Count, Util.GetFooterInfo(this));
			return Util.ConvertToJson(response, this, list);
		}

		private string GetSortExpression(string sortExpression, string sortDirection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<string> list = sortExpression.Split(',').ToList();
			foreach (string item in list)
			{
				string arg = sortDirection;
				if (item.Trim().Length == 0)
				{
					break;
				}
				List<string> list2 = item.Trim().Split(' ').ToList();
				string arg2 = list2[0];
				if (list2.Count > 1)
				{
					arg = list2[1];
				}
				if (list2.Count > 1)
				{
					string text = list2[1];
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}

			    // should be used custom sort values?
			    var col = Columns.FirstOrDefault(t => !string.IsNullOrEmpty(t.SortByColumnNameValues) && t.DataField == arg2);
			    if (col != null)
			    {
			        if (Columns.All(t => t.DataField != col.SortByColumnNameValues))
			        {
			            throw new Exception(string.Format("Column with DataField {0} couldn't be found. It's required to use as custom sorter", col.SortByColumnNameValues));
			        }
			        stringBuilder.AppendFormat("{0} {1}", col.SortByColumnNameValues, arg);
			    }
			    else
			    {
			        stringBuilder.AppendFormat("{0} {1}", arg2, arg);
			    }

                
			}
			return stringBuilder.ToString();
		}

		private JsonResult GetJsonResponse()
		{
			HttpRequest request = Context.Request;
			Guard.IsNotNull(DataSource, "DataSource");
			IQueryable iqueryable;
			return FilterDataSource(DataSource, request.Query.ToDictionary(), out iqueryable);
		}

		public CoreGridEditData GetEditData()
		{
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			HttpRequest request = Context.Request;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string key in request.Form.Keys)
			{
				if (key != "oper")
				{
					dictionary[key] = request.Form[key];
				}
			}
			string text = string.Empty;
			foreach (CoreColumn column in Columns)
			{
				if (column.PrimaryKey)
				{
					text = column.DataField;
					break;
				}
			}
			if (dictionary.ContainsKey("id") && !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(dictionary["id"]))
			{
				dictionary[text] = dictionary["id"];
			}
			CoreGridEditData coreGridEditData = new CoreGridEditData();
			coreGridEditData.RowData = dictionary;
			coreGridEditData.RowKey = dictionary[text];
			string text2 = request.Query["parentRowID"];
			if (!string.IsNullOrEmpty(text2))
			{
				coreGridEditData.ParentRowKey = text2;
			}
			return coreGridEditData;
		}

		public CoreGridTreeExpandData GetTreeExpandData()
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			CoreGridTreeExpandData coreGridTreeExpandData = new CoreGridTreeExpandData();
			HttpRequest request = Context.Request;
			if (request.Query.ContainsKey("nodeid"))
			{
				coreGridTreeExpandData.ParentID = ((IEnumerable<string>)(object)request.Query["nodeid"]).First();
			}
			if (request.Query.ContainsKey("n_level"))
			{
				coreGridTreeExpandData.ParentLevel = Convert.ToInt32(((IEnumerable<string>)(object)request.Query["nodeid"]).First());
			}
			return coreGridTreeExpandData;
		}

		public IQueryable GetFilteredDataSource(object dataSource, CoreGridState gridState)
		{
			if (ExportSettings.ExportDataRange != ExportDataRange.FilteredAndPaged)
			{
				gridState.QueryString["page"] = "1";
				gridState.QueryString["rows"] = "1000000";
			}
			FilterDataSource(dataSource, gridState.QueryString, out IQueryable iqueryable);
			return iqueryable;
		}

		public CoreGridState GetState()
		{
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string key in Context.Request.Query.Keys)
			{
				dictionary.Add(key, Context.Request.Query[key]);
			}
			return new CoreGridState
			{
				QueryString = dictionary
			};
		}

	    public Stream ExportToCSV(IQueryable dataSource)
		{
			List<Hashtable> data = Util.ToListOfHashtables(dataSource);
			return RenderCSVToStream(data);
		}

	    public Stream ExportToCSV(object dataSource, CoreGridState gridState)
		{
			IQueryable filteredDataSource = GetFilteredDataSource(dataSource, gridState);
			return ExportToCSV(filteredDataSource);
		}

	    public Stream ExportToCSV<TModel>(IQueryable dataSource)
	    {
	        List<Hashtable> data = Util.ToListOfHashtables<TModel>(dataSource);
	        return RenderCSVToStream(data);
	    }

	    public Stream ExportToCSV<TModel>(object dataSource, CoreGridState gridState)
	    {
	        IQueryable filteredDataSource = GetFilteredDataSource(dataSource, gridState);
	        List<Hashtable> data = Util.ToListOfHashtables<TModel>(filteredDataSource);
            return RenderCSVToStream(data);
	    }

        protected Stream RenderCSVToStream(List<Hashtable> data)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.Unicode);
			streamWriter.Write(GetOutput(this, data));
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return memoryStream;
		}

		private string GetOutput(CoreGrid grid, List<Hashtable> data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (ExportSettings.ExportHeaders)
			{
				foreach (CoreColumn column in grid.Columns)
				{
					stringBuilder.AppendFormat("{0}{1}", QuoteText(column.DataField), ExportSettings.CSVSeparator);
				}
			}
			stringBuilder.Append("\n");
			for (int i = 0; i < data.Count; i++)
			{
				for (int j = 0; j < Columns.Count; j++)
				{
					if (Columns[j].Visible)
					{
						CoreColumn coreColumn = grid.Columns[j];
						string text = (!string.IsNullOrEmpty(coreColumn.DataField) && !string.IsNullOrEmpty(coreColumn.DataFormatString)) ? coreColumn.FormatDataValue(data[i][coreColumn.DataField], coreColumn.HtmlEncode) : (data[i][coreColumn.DataField] as string);
						stringBuilder.AppendFormat("{0}{1}", data[i][coreColumn.DataField], ExportSettings.CSVSeparator);
					}
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		public string QuoteText(string input)
		{
			return string.Format("\"{0}\"", input.Replace("\"", "\"\"").Replace("&nbsp;", ""));
		}

	    public Stream ExportToExcel(IQueryable dataSource)
		{
			List<Hashtable> data = Util.ToListOfHashtables(dataSource);
			return RenderExcelToStream(data);
		}

	    public Stream ExportToExcel(object dataSource, CoreGridState gridState)
		{
			IQueryable filteredDataSource = GetFilteredDataSource(dataSource, gridState);
			return ExportToExcel(filteredDataSource);
		}
	    public Stream ExportToExcel<TModel>(IQueryable dataSource)
	    {
	        List<Hashtable> data = Util.ToListOfHashtables<TModel>(dataSource);
	        return RenderExcelToStream(data);
	    }

	    public Stream ExportToExcel<TModel>(object dataSource, CoreGridState gridState)
	    {
	        IQueryable filteredDataSource = GetFilteredDataSource(dataSource, gridState);
	        List<Hashtable> data = Util.ToListOfHashtables<TModel>(filteredDataSource);
	        return RenderExcelToStream(data);
        }

        private Stream RenderExcelToStream(List<Hashtable> data)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.Unicode);
            streamWriter.Write(this.ExcelRenderSettingsRule != null
                ? ExcelRenderSettingsRule.GetHtmlTable(this, data)
                : GetHtmlTable(this, data));
            streamWriter.Flush();
			memoryStream.Position = 0L;
			return memoryStream;
		}

		private string GetHtmlTable(CoreGrid grid, List<Hashtable> data)
		{
			StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
                "<html><head><style> table, td {border:1px solid black} table { border-collapse:collapse; background:transparent}</style></head> ");

			stringBuilder.Append("<table>");
			stringBuilder.Append("<tr>");
			foreach (CoreColumn column in grid.Columns)
			{
				if (column.Visible)
				{
					string arg = string.IsNullOrEmpty(column.HeaderText) ? column.DataField : column.HeaderText;
                    string sizeRowPx = column.ExcelWidthRowPx != 0 ? column.ExcelWidthRowPx + "px;" : "";
                    if (!string.IsNullOrWhiteSpace(sizeRowPx))
                    {
                        stringBuilder.AppendFormat("<td style=\"width: {0}\">{1}</td>",sizeRowPx, arg);
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<td>{0}</td>", arg);
                    }
                }
			}
			stringBuilder.Append("</tr>");
			for (int i = 0; i < data.Count; i++)
			{
				stringBuilder.Append("<tr>");
				for (int j = 0; j < grid.Columns.Count; j++)
				{
				    CoreColumn coreColumn = grid.Columns[j];
                    if (coreColumn.Visible)
				    {
				        string arg2 =
				            (!string.IsNullOrEmpty(coreColumn.DataField) &&
				             !string.IsNullOrEmpty(coreColumn.DataFormatString))
				                ? coreColumn.FormatDataValue(data[i][coreColumn.DataField], coreColumn.HtmlEncode)
				                : (data[i][coreColumn.DataField]?.ToString());
				        stringBuilder.AppendFormat("<td>{0}</td>", arg2);
				    }
				}

				stringBuilder.Append("</tr>");
			}
			stringBuilder.Append("</table>");
			return stringBuilder.ToString();
		}
	}
}
