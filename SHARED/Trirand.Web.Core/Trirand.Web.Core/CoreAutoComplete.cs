using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreAutoComplete
	{
		private HttpContext _httpContext;

		public AutoCompleteMode AutoCompleteMode
		{
			get;
			set;
		}

		public string DataField
		{
			get;
			set;
		}

		public object DataSource
		{
			get;
			set;
		}

		public string DataUrl
		{
			get;
			set;
		}

		public AutoCompleteDisplayMode DisplayMode
		{
			get;
			set;
		}

		public bool AutoFocus
		{
			get;
			set;
		}

		public int Delay
		{
			get;
			set;
		}

		public bool Enabled
		{
			get;
			set;
		}

		public string ID
		{
			get;
			set;
		}

		public int MinLength
		{
			get;
			set;
		}

		public CoreAutoComplete()
		{
			AutoCompleteMode = AutoCompleteMode.BeginsWith;
			DataField = "";
			DataSource = null;
			DataUrl = "";
			DisplayMode = AutoCompleteDisplayMode.Standalone;
			ID = "";
			AutoFocus = false;
			Delay = 300;
			Enabled = true;
			MinLength = 1;
		}

		public string DataBind(object dataSource, HttpContext httpContext)
		{
			DataSource = dataSource;
			return DataBind(httpContext);
		}

		public string DataBind(HttpContext httpContext)
		{
			_httpContext = httpContext;
			return GetJsonResponse();
		}

		private string GetJsonResponse()
		{
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			Guard.IsNotNull(DataSource, "DataSource");
			IQueryable queryable = DataSource as IQueryable;
			Guard.IsNotNull(queryable, "DataSource", "should implement the IQueryable interface.");
			Guard.IsNotNullOrEmpty(DataField, "DataField", "should be set to the datafield (column) of the datasource to search in.");
			SearchOperation searchOperation = SearchOperation.IsEqualTo;
			searchOperation = ((AutoCompleteMode != 0) ? SearchOperation.Contains : SearchOperation.BeginsWith);
			string text = _httpContext.Request.Query["term"];
			if (!string.IsNullOrEmpty(text))
			{
				queryable = queryable.Where(Util.ConstructLinqFilterExpression(this, new Util.SearchArguments
				{
					SearchColumn = DataField,
					SearchOperation = searchOperation,
					SearchString = text
				}));
			}
			List<string> list = new List<string>();
			List<Hashtable> list2 = Util.ToListOfHashtables(queryable);
			foreach (Hashtable item in list2)
			{
				if (item[DataField] != null)
				{
					list.Add(item[DataField] as string);
				}
				else
				{
					list.Add(string.Empty);
				}
			}
			return JsonConvert.SerializeObject((object)list);
		}
	}
}
