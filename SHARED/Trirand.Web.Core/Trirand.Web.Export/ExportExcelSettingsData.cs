using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Trirand.Web.Core.Trirand.Web.Core;

namespace Trirand.Web.Core.Trirand.Web.Export
{
    //TODO если пойдет в прод. дописать доп. св-ва
    public sealed class ExportExcelSettingsData
    {
        internal int TableColumnWidth { get; set; }

        internal int TableColumnHeight { get; set; }

        internal TextAlign TableHeaderColumnTextAlign = TextAlign.Center;
        internal FontWeight TableHeaderColumnFontWeight = FontWeight.Normal;

        internal TextAlign RowCellTextAlign = TextAlign.Left;

        internal string CustomStyle { get; set; }

    }

    public static class ExportExcelSettingsDataExtension
    {
        public static ExportExcelSettingsData TableColumnWidth(this ExportExcelSettingsData @this, int value)
        {
            @this.TableColumnWidth = value;
            return @this;
        }
        public static ExportExcelSettingsData TableColumnHeight(this ExportExcelSettingsData @this, int value)
        {
            @this.TableColumnHeight = value;
            return @this;
        }
        public static ExportExcelSettingsData TableHeaderColumnTextAlign(this ExportExcelSettingsData @this, TextAlign value)
        {
            @this.TableHeaderColumnTextAlign = value;
            return @this;
        }
        public static ExportExcelSettingsData TableHeaderColumnFontWeight(this ExportExcelSettingsData @this, FontWeight value)
        {
            @this.TableHeaderColumnFontWeight = value;
            return @this;
        }

        public static ExportExcelSettingsData CellTextAlign(this ExportExcelSettingsData @this, TextAlign value)
        {
            @this.RowCellTextAlign = value;
            return @this;
        }
        public static ExportExcelSettingsData CustomStyle(this ExportExcelSettingsData @this, string value)
        {
            @this.CustomStyle = value;
            return @this;
        }
    }

    public enum FontWeight
    {
        Bold,
        Bolder,
        Lighter,
        Normal
    }
}
