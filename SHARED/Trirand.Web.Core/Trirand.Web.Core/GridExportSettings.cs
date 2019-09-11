namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class GridExportSettings
	{
		public string CSVSeparator
		{
			get;
			set;
		}

		public string ExportUrl
		{
			get;
			set;
		}

		public bool ExportHeaders
		{
			get;
			set;
		}

		public ExportDataRange ExportDataRange
		{
			get;
			set;
		}

		public GridExportSettings()
		{
			ExportUrl = "";
			CSVSeparator = ",";
			ExportHeaders = true;
			ExportDataRange = ExportDataRange.All;
		}
	}
}
