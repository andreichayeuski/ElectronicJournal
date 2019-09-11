using System.Collections;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonTreeResponse
	{
		public int page
		{
			get;
			set;
		}

		public int total
		{
			get;
			set;
		}

		public int records
		{
			get;
			set;
		}

		public Hashtable[] rows
		{
			get;
			set;
		}

		public Hashtable userdata
		{
			get;
			set;
		}

		public JsonTreeResponse()
		{
		}

		public JsonTreeResponse(int currentPage, int totalPagesCount, int totalRowCount, int pageSize, int actualRows, Hashtable userData)
		{
			page = currentPage;
			total = totalPagesCount;
			records = totalRowCount;
			rows = new Hashtable[actualRows];
			userdata = userData;
		}
	}
}
