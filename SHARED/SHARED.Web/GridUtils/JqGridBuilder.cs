using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SHARED.Common.Extensions;
using SHARED.Common.Utils;
using SHARED.Models.Attributes;
using Trirand.Web.Core.Trirand.Web.Core;

namespace SHARED.Web.GridUtils
{
    public class CoreGridFor<TModel> : CoreGrid where TModel : class
    {
        /// <summary>
        ///     formatEditName = null значит что не будет колонки с действиями
        /// </summary>
        public CoreGridFor(HttpContext context, string formatEditName = "formatEdit") : base(context)
        {
            AutoWidth = true;
            Height = "100%";
            Width = "100%";
            PagerSettings = new PagerSettings {PageSizeOptions = $"[10,20,50,100,500,1000,5000,{JqGridHelper.GridAllRecordsCount}]"};
            SearchDialogSettings = new SearchDialogSettings {MultipleSearch = true, Width = 800};

            var columnModels = GetModelColumns(typeof(TModel));
            var primaryKeyColumn = columnModels.FirstOrDefault(t => t.AttributeInfo.PrimaryKeyField);

            SortSettings = new SortSettings
            {
                InitialSortColumn = primaryKeyColumn != null ? primaryKeyColumn.PropertyInfo.Name : "Id",
                InitialSortDirection = SortDirection.Asc
            };
            ToolBarSettings = new ToolBarSettings
            {
                ShowSearchToolBar = true,
                ShowSearchButton = true,
                ShowRefreshButton = true,
                ShowEditButton = false,
                ShowAddButton = false,
                ShowDeleteButton = false
            };

            SetColumn(columnModels, formatEditName);

            EditDialogSettings.CloseAfterEditing = true;
            AddDialogSettings.CloseAfterAdding = true;

            Responsive = true;
            ColumnReordering = true;
            StoreNavigationOptions = true;
            AppearanceSettings.ShowRowNumbers = true;

            ID = "JqGrid" + typeof(TModel).Name + "s";

            SearchToolBarSettings.SearchOperators = true;
            SearchToolBarSettings.SearchToolBarAction = SearchToolBarAction.SearchOnEnter;
            //TreeGridSettings.Enabled = true;
        }

        private IList<JqGridBuilderColumnModel> GetModelColumns(Type type, PropertyInfo parent = null)
        {
            Func<PropertyInfo, JqGridBuilderColumnModel> getJqGridBuilderColumnModel = z => new JqGridBuilderColumnModel
            {
                PropertyInfo = z,
                AttributeInfo = ReflectionHelper.GetAttribute<GridColumnAttribute>(z),
                UiHintInfo = ReflectionHelper.GetAttribute<UIHintAttribute>(z),
                Parent = parent
            };

            var columnModels = type.GetProperties().Where(z => !z.HasAttribute<GridExcludedPropertyAttribute>())
                .Union(type.GetInterfaces().SelectMany(i =>
                    i.GetProperties().Where(z => !z.HasAttribute<GridExcludedPropertyAttribute>()))).SelectMany(z =>
                    z.HasAttribute<GridIncludedPropertyAttribute>()
                        ? GetModelColumns(z.PropertyType, z).Select(t =>
                        {
                            t.AttributeInfo.Hidden =
                                ReflectionHelper.GetAttribute<GridIncludedPropertyAttribute>(z).Hidden;
                            return t;
                        })
                        : new[] {getJqGridBuilderColumnModel(z)})
                .Where(z => z.AttributeInfo != null)
                .OrderBy(z => z.AttributeInfo.OrderPriority)
                .ToList();

            return columnModels;
        }

        private void SetColumn(IList<JqGridBuilderColumnModel> columnModels, string formatEditName)
        {
            Columns = columnModels.Select(GetColumn).ToList();

            if (formatEditName != null)
                Columns.Add(new CoreColumn
                {
                    Searchable = false,
                    TextAlign = TextAlign.Center,
                    HeaderText = "", // было слово Действия - мне не надо в ФОР
                    Width = 70,
                    ExportToExcel = false,
                    Formatter = new CustomFormatter
                    {
                        FormatFunction = formatEditName
                    }
                });

            if (columnModels.Any(c => c.AttributeInfo.IsGroupColumn))
                GroupSettings = new GroupSettings
                {
                    GroupFields = columnModels.Where(c => c.AttributeInfo.IsGroupColumn).Select(z => new GroupField
                    {
                        DataField = z.AttributeInfo.DataField ?? z.PropertyInfo.Name
                    }).ToList()
                };

            if (columnModels.Any(c => c.AttributeInfo.ColumnMenu))
                ColumnMenu = true;

            if (columnModels.Any(c => c.AttributeInfo.Searchable))
                ToolBarSettings.ShowSearchToolBar = true;
        }

        private static void SetSearchOperatorsIntColumn(CoreColumn jqColumn, JqGridBuilderColumnModel columnModel)
        {
            jqColumn.SearchToolBarOperation =
                columnModel.AttributeInfo.DefaultSearchToolBarOperation ?? SearchOperation.IsEqualTo;
            jqColumn.SearchOptions = JqGridHelper.GetSearchOperators(typeof(int));
        }

        private static void SetSearchOperatorsStringColumn(CoreColumn jqColumn, JqGridBuilderColumnModel columnModel)
        {
            //#6188
            //jqColumn.SearchToolBarOperation =
            //    columnModel.AttributeInfo.DefaultSearchToolBarOperation ?? SearchOperation.Contains;
            jqColumn.SearchOptions = JqGridHelper.GetSearchOperators(typeof(string));
            jqColumn.SearchToolBarOperation = SearchOperation.Contains;
            jqColumn.ShowSearchOperators = false;

        }

        private void SetSearchOperatorsDecimalColumn(CoreColumn jqColumn, JqGridBuilderColumnModel columnModel)
        {
            SetSearchOperatorsIntColumn(jqColumn, columnModel);

            columnModel.AttributeInfo.DecimalFormat = string.IsNullOrEmpty(columnModel.AttributeInfo.DecimalFormat)
                ? "n2"
                : columnModel.AttributeInfo.DecimalFormat;

            var decumalFormatterFunction =
                "function(cell){try{var globalValue=Globalize.parseFloat(cell);if(isNaN(globalValue))" +
                "return '';" +
                "return Globalize.format(globalValue,'" + columnModel.AttributeInfo.DecimalFormat + "');}catch(e){}}";

            jqColumn.Formatter = new CustomFormatter
            {
                FormatFunction = string.IsNullOrEmpty(columnModel.AttributeInfo.CustomFormatFunction)
                    ? decumalFormatterFunction
                    : columnModel.AttributeInfo.CustomFormatFunction
            };
        }

        private void SetSearchOperatorsBoolColumn(CoreColumn jqColumn, JqGridBuilderColumnModel columnModel)
        {
            jqColumn.Formatter = new CheckBoxFormatter {Enabled = jqColumn.Editable};
            jqColumn.SearchToolBarOperation =
                columnModel.AttributeInfo.DefaultSearchToolBarOperation ?? SearchOperation.IsEqualTo;
            jqColumn.ShowSearchOperators = false;
            jqColumn.SearchOptions = JqGridHelper.GetSearchOperators(typeof(bool));

            jqColumn.SearchType = SearchType.DropDown;
            jqColumn.SearchList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Все", Value = null},
                new SelectListItem {Text = "Да", Value = true.ToString()},
                new SelectListItem {Text = "Нет", Value = false.ToString()}
            };
        }

        private void SetSearchOperatorsDateTimeColumn(CoreColumn jqColumn, JqGridBuilderColumnModel columnModel)
        {
            if (columnModel.AttributeInfo.NeedSearchEditorControl)
                jqColumn.SearchType = SearchType.DatePicker;

            jqColumn.SearchToolBarOperation =
                columnModel.AttributeInfo.DefaultSearchToolBarOperation ?? SearchOperation.IsEqualTo;
            jqColumn.SearchOptions = JqGridHelper.GetSearchOperators(typeof(DateTime));
            jqColumn.DataFormatString = string.IsNullOrEmpty(columnModel.AttributeInfo.DateFormatString)
                ? "{0:dd.MM.yyyy}"
                : columnModel.AttributeInfo.DateFormatString;
        }

        private CoreColumn GetColumn(JqGridBuilderColumnModel columnModel)
        {
            Func<JqGridBuilderColumnModel, string> getDataField = c =>
            {
                var parentNameField = string.Empty;
                if (c.Parent != null) parentNameField = c.Parent.Name + ".";
                return parentNameField + (c.AttributeInfo.DataField ?? c.PropertyInfo.Name);
            };

            var jqColumn = new CoreColumn
            {
                // временное решение для смарт реестра
                GroupTemplate = columnModel.Parent != null
                    ? columnModel.Parent.GetAttributes<GridIncludedPropertyAttribute>().Select(t => t.ColumnName)
                        .FirstOrDefault()
                    : null,
                //
                DataType = columnModel.AttributeInfo.DataType ?? columnModel.PropertyInfo.PropertyType,
                DataField = getDataField(columnModel),
                PrimaryKey = columnModel.AttributeInfo.PrimaryKeyField || columnModel.PropertyInfo.Name == "Id",
                Editable = columnModel.AttributeInfo.Editable,
                EditType = columnModel.AttributeInfo.EditType,
                Searchable = !columnModel.AttributeInfo.PrimaryKeyField && columnModel.PropertyInfo.Name != "Id" &&
                             columnModel.AttributeInfo.Searchable,
                HeaderText = GetHeaderText(columnModel.PropertyInfo),
                TextAlign = TextAlign.Center,
                Visible = !columnModel.AttributeInfo.PrimaryKeyField && columnModel.PropertyInfo.Name != "Id" &&
                          !columnModel.AttributeInfo.Hidden,
                Width = columnModel.AttributeInfo.Width,
                ShowSearchOperators = columnModel.AttributeInfo.ShowSearchOperators,
                SortByColumnNameValues = columnModel.AttributeInfo.SortByColumnNameValues ?? string.Empty,
                ShowColumnMenu = columnModel.AttributeInfo.ColumnMenu,
                //TemplateRendererViewName = "",
                //TemplateRendererViewName =  columnModel.UiHintInfo != null && !columnModel.AttributeInfo.DisableUiHintTemplateRenderer ? columnModel.UiHintInfo.UIHint : string.Empty,//временно отключил
                ColumnMenuOptions = new ColumnMenuOptions {Columns = true, Sorting = true}, // move it to attr?
                ExportToExcel = !columnModel.AttributeInfo.PrimaryKeyField && columnModel.PropertyInfo.Name != "Id" &&
                                columnModel.AttributeInfo.ExportToExcel,
                SearchDataField = columnModel.AttributeInfo.SearchDataField,
                Sortable = columnModel.AttributeInfo.Sortable
            };

            if (jqColumn.DataType == typeof(bool) || jqColumn.DataType == typeof(bool?))
                SetSearchOperatorsBoolColumn(jqColumn, columnModel);
            else if (jqColumn.DataType == typeof(DateTime) ||
                     jqColumn.DataType == typeof(DateTime?))
                SetSearchOperatorsDateTimeColumn(jqColumn, columnModel);
            else if (jqColumn.DataType == typeof(decimal) ||
                     jqColumn.DataType == typeof(decimal?))
                SetSearchOperatorsDecimalColumn(jqColumn, columnModel);
            else if (jqColumn.DataType == typeof(int) ||
                     jqColumn.DataType == typeof(int?))
                SetSearchOperatorsIntColumn(jqColumn, columnModel);
            else if (jqColumn.DataType == typeof(string)) SetSearchOperatorsStringColumn(jqColumn, columnModel);

            if (!string.IsNullOrEmpty(columnModel.AttributeInfo.CustomFormatFunction))
                jqColumn.Formatter = new CustomFormatter
                {
                    FormatFunction = columnModel.AttributeInfo.CustomFormatFunction
                };


            if (columnModel.AttributeInfo.DataType == null)
            {
                if (columnModel.PropertyInfo.PropertyType == typeof(int))
                {
                    jqColumn.SearchToolBarOperation =
                        columnModel.AttributeInfo.DefaultSearchToolBarOperation ?? SearchOperation.IsEqualTo;
                    jqColumn.SearchOptions = new List<SearchOperation>
                    {
                        SearchOperation.IsEqualTo
                    };
                }
            }
            else
            {
                if (columnModel.AttributeInfo.DataType == typeof(int))
                {
                    jqColumn.SearchToolBarOperation =
                        columnModel.AttributeInfo.DefaultSearchToolBarOperation ?? SearchOperation.IsEqualTo;
                    jqColumn.SearchOptions = new List<SearchOperation>
                    {
                        SearchOperation.IsEqualTo
                    };
                }
            }

            if (columnModel.AttributeInfo.MultipleSearch)
            {
                jqColumn.SearchToolBarOperation =
                    columnModel.AttributeInfo.DefaultSearchToolBarOperation ?? SearchOperation.IsIn;
                if (jqColumn.SearchOptions != null)
                    jqColumn.SearchOptions.Add(SearchOperation.IsIn);
            }

            jqColumn.SearchOptions =
                jqColumn.SearchOptions.OrderByDescending(t => t == jqColumn.SearchToolBarOperation).ThenBy(t => t)
                    .ToList();

            if (columnModel.AttributeInfo.CustomSearchOperations != null &&
                columnModel.AttributeInfo.CustomSearchOperations.Any())
                jqColumn.SearchOptions = columnModel.AttributeInfo.CustomSearchOperations.ToList();

            return jqColumn;
        }

        private static string GetHeaderText(PropertyInfo property)
        {
            var dAttr = ReflectionHelper.GetAttribute<DisplayAttribute>(property);
            var grAttr = ReflectionHelper.GetAttribute<GridColumnAttribute>(property);

            return grAttr != null && !string.IsNullOrEmpty(grAttr.ColumnName)
                ? grAttr.ColumnName
                : (dAttr != null ? dAttr.Name : property.Name);
        }

        private class JqGridBuilderColumnModel
        {
            public PropertyInfo PropertyInfo { get; set; }
            public GridColumnAttribute AttributeInfo { get; set; }
            public UIHintAttribute UiHintInfo { get; set; }
            public PropertyInfo Parent { get; set; }
        }
    }

    public class JqGridBuilder<TModel> where TModel : class
    {
        public static CoreGrid GetBasicGrid(HttpContext context, string formatEditName = "formatEdit")
        {
            var grid = new CoreGridFor<TModel>(context, formatEditName);
            return grid;
        }
    }

    public class JqGridHelper
    {
        public const int GridAllRecordsCount = 100000000;

        public static void AddSearchDropdown(CoreColumn column, List<SelectListItem> items,
            SearchOperation searchOperation = SearchOperation.IsEqualTo, bool needAllItem = true)
        {
            column.SearchToolBarOperation = searchOperation;
            column.SearchOptions = new List<SearchOperation>
            {
                searchOperation
            };
            column.SearchType = SearchType.DropDown;
            var searchList = new List<SelectListItem>();

            if (needAllItem)
                searchList.Add(new SelectListItem {Text = "Все", Value = null});

            searchList.AddRange(items);

            column.ShowSearchOperators = false;
            column.SearchList = searchList;
        }

        public static List<SearchOperation> GetSearchOperators(Type type)
        {
            if (type == typeof(int) || type == typeof(int?) || type == typeof(double) || type == typeof(double?) ||
                type == typeof(decimal) || type == typeof(decimal?) || type == typeof(long) || type == typeof(long?))
                return new List<SearchOperation>
                {
                    SearchOperation.IsEqualTo,
                    SearchOperation.IsGreaterOrEqualTo,
                    SearchOperation.IsLessOrEqualTo,
                    SearchOperation.IsGreaterThan,
                    SearchOperation.IsLessThan
                };
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return new List<SearchOperation>
                {
                    SearchOperation.IsEqualTo,
                    SearchOperation.IsGreaterOrEqualTo,
                    SearchOperation.IsLessOrEqualTo,
                    SearchOperation.IsGreaterThan,
                    SearchOperation.IsLessThan
                };
            if (type == typeof(string))
                return new List<SearchOperation>
                {
                    SearchOperation.Contains,
                    //#6188
                    //SearchOperation.IsEqualTo, 
                    //SearchOperation.DoesNotContain,
                    //SearchOperation.BeginsWith,
                    //SearchOperation.EndsWith
                };
            if (type == typeof(bool) || type == typeof(bool?))
                return new List<SearchOperation>
                {
                    SearchOperation.IsEqualTo
                };

            return new List<SearchOperation>(); // empty
        }
    }
}