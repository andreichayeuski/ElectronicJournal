using System;
using Trirand.Web.Core.Trirand.Web.Core;

namespace SHARED.Models.Attributes
{
    public class PrimaryKeyColumnAttribute : GridColumnAttribute
    {
        public PrimaryKeyColumnAttribute()
        {
            Hidden = true;
            PrimaryKeyField = true;
        }
    }

    public class SearchGridColumnAttribute : GridColumnAttribute
    {
        public SearchGridColumnAttribute(SearchOperation defaultSearchToolBarOperation)
        {
            Hidden = true;
            DefaultSearchToolBarOperation = defaultSearchToolBarOperation;
        }
    }

    public class GridIncludedPropertyAttribute : Attribute
    {
        public GridIncludedPropertyAttribute(string columnName)
        {
            ColumnName = columnName;
            Hidden = false;
        }

        public string ColumnName { get; set; }
        public bool Hidden { get; set; }
    }

    public class GridExcludedPropertyAttribute : Attribute
    {
    }

    public class GridColumnAttribute : Attribute
    {
        public GridColumnAttribute()
        {
            ColumnName = null;
            Searchable = true;
            ShowSearchOperators = true;
            NeedSearchEditorControl = true;
            ExportToExcel = true;
            Sortable = true;
            EditType = EditType.TextBox; /* default */
        }

        public GridColumnAttribute(string columnName) : this()
        {
            ColumnName = columnName;
        }

        public GridColumnAttribute(string columnName, string dateFormatString)
        {
            ColumnName = columnName;
            Searchable = true;
            DateFormatString = dateFormatString;
            NeedSearchEditorControl = true;
            ShowSearchOperators = true;
            ExportToExcel = true;
            Sortable = true;
        }

        public GridColumnAttribute(string columnName, int orderPriority)
        {
            ColumnName = columnName;
            Searchable = true;
            OrderPriority = orderPriority;
            NeedSearchEditorControl = true;
            ExportToExcel = true;
            ShowSearchOperators = true;
            Sortable = true;
        }

        public GridColumnAttribute(string columnName, int orderPriority, string dateFormatString)
        {
            ColumnName = columnName;
            Searchable = true;
            DateFormatString = dateFormatString;
            OrderPriority = orderPriority;
            NeedSearchEditorControl = true;
            ExportToExcel = true;
            ShowSearchOperators = true;
            Sortable = true;
        }

        public string ColumnName { get; }
        public bool PrimaryKeyField { get; set; }
        public string DateFormatString { get; set; } //= "{0:dd.MM.yyyy}" 
        public int OrderPriority { get; set; }
        public int Width { get; set; }

        public Type DataType { get; set; }

        public string DataField { get; set; }

        public string CustomFormatFunction { get; set; }

        public bool IsGroupColumn { get; set; }

        public bool Searchable { get; set; }
        public bool Sortable { get; set; }

        public bool Hidden { get; set; }

        public bool ShowSearchOperators { get; set; }

        /// <summary>
        ///     Поиск сразу по нескольким значениям данных
        /// </summary>
        public bool MultipleSearch { get; set; }

        /// <summary>
        ///     Нужно ли меню для столбца в Header
        /// </summary>
        public bool ColumnMenu { get; set; }

        public bool NeedSearchEditorControl { get; set; }

        /// <summary>
        ///     Нужно ли экспортировать в Excel
        /// </summary>
        public bool ExportToExcel { get; set; }

        public bool Editable { get; set; }

        /// <summary>
        ///     Сортировка по другим полям
        /// </summary>
        public string SortByColumnNameValues { get; set; }

        /// <summary>
        ///     Формат для decimal чисел (например N2)
        /// </summary>
        public string DecimalFormat { get; set; }

        /// <summary>
        ///     Отключить TemplateRenderer исходя из UIHint
        /// </summary>
        public bool DisableUiHintTemplateRenderer { get; set; }

        /// <summary>
        ///     По умолчанию какой оператор использовать, если не задавать, будет использоваться тот что определено по типу
        /// </summary>
        public SearchOperation? DefaultSearchToolBarOperation { get; set; }

        /// <summary>
        ///     по какому полю осуществлять поиск при вводе в данную ячейку
        /// </summary>
        public string SearchDataField { get; set; }

        public SearchOperation[] CustomSearchOperations { get; set; }

        /// <summary>
        ///  тип поля для редактирования
        /// </summary>
        public EditType EditType { get; set; }

        /// TODO можно будет придумать что-то с ещё с форматтерами, размерами, порядком

    }
}