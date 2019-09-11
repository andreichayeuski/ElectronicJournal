using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Trirand.Web.Core.Trirand.Web.Core;

namespace Trirand.Web.Core.Trirand.Web.Export
{
    public interface IExcelRenderSettingsRule
    {
        string GetHtmlTable(CoreGrid grid, List<Hashtable> data);
    }

    public abstract class ExcelRenderSettingsRule <TUiGridModel> : IExcelRenderSettingsRule 
                                                                   where TUiGridModel : class
    {
        private readonly Dictionary<MemberInfo, ExportExcelSettingsData> _exportExcelSettings = new Dictionary<MemberInfo, ExportExcelSettingsData>();

        protected ExportExcelSettingsData RuleFor <TProperty>(Expression<Func<TUiGridModel, TProperty>> expression)
        {
            var exportExcelSettingsData = new ExportExcelSettingsData();
            _exportExcelSettings.Add(expression.GetMember(), exportExcelSettingsData); 
            return exportExcelSettingsData;
        }
        public virtual string GetHtmlTable(CoreGrid grid, List<Hashtable> data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
                "<html><head><style> table, td {border:1px solid black} table { border-collapse:collapse; background:transparent} .text{ mso-number-format:\"\\@\";/*force text*/ }</style></head> ");
            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr>");
            foreach (CoreColumn column in grid.Columns)
            {
                if (column.Visible)
                {
                    string arg = string.IsNullOrEmpty(column.HeaderText) ? column.DataField : column.HeaderText;
                    var styleSettings = _exportExcelSettings.FirstOrDefault(t => t.Key.Name == column.DataField);
                    if (styleSettings.Value != null)
                    {
                        stringBuilder.AppendFormat("<td class=\"text\" style=\"{0}\"\">{1}</td>", BuildColumnStyle(styleSettings.Value, RenderExcelElementType.Header), arg);
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<td class=\"text\">{0}</td>", arg);
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
                                : ( data[i][coreColumn.DataField]?.ToString());
                        var styleSettings = _exportExcelSettings.FirstOrDefault(t => t.Key.Name == coreColumn.DataField);

                        if (styleSettings.Value != null)
                        {
                            stringBuilder.AppendFormat("<td class=\"text\" style=\"{0}\"\">{1}</td>", BuildColumnStyle(styleSettings.Value, RenderExcelElementType.Row), arg2);
                        }
                        else
                        {
                            //http://cosicimiento.blogspot.com/2008/11/styling-excel-cells-with-mso-number.html
                            stringBuilder.AppendFormat("<td class=\"text\">{0}</td>", arg2);
                        }
                    }
                }

                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }

        private StringBuilder BuildColumnStyle(ExportExcelSettingsData data, RenderExcelElementType renderExcelElementType)
        {
            StringBuilder buildedStyle = new StringBuilder();

            if (data.TableColumnHeight > 0)
            {
                buildedStyle.AppendFormat($"height: {data.TableColumnHeight}px;");
            }

            if (data.TableColumnWidth > 0)
            {
                buildedStyle.AppendFormat($"width: {data.TableColumnWidth}px;");
            }

            if (renderExcelElementType == RenderExcelElementType.Header)
            {
                buildedStyle.AppendFormat($"text-align: {data.TableHeaderColumnTextAlign.ToString()};");
                if (data.TableHeaderColumnTextAlign == TextAlign.Center)
                {
                    buildedStyle.AppendFormat("vertical-align: middle;");
                }
            }

            if (renderExcelElementType == RenderExcelElementType.Row)
            {
                if (data.RowCellTextAlign == TextAlign.Left)
                {
                    buildedStyle.AppendFormat("vertical-align: top;");
                }
            }
            
            buildedStyle.AppendFormat($"font-weight: {data.TableHeaderColumnFontWeight.ToString()};");

            if (!String.IsNullOrEmpty(data.CustomStyle))
            {
                buildedStyle.Append(data.CustomStyle);
            }
            return buildedStyle;
        }
    }
}
