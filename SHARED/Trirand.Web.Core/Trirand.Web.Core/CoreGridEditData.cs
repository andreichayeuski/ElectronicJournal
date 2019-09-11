using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreGridEditData
	{
		public Dictionary<string, string> RowData
		{
			get;
			set;
		}

		public string RowKey
		{
			get;
			set;
		}

		public string ParentRowKey
		{
			get;
			set;
		}
	}
}
