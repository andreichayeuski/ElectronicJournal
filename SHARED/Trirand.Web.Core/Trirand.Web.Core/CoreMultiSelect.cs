using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreMultiSelect
	{
		public string ID
		{
			get;
			set;
		}

		public List<CoreListItem> Items
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public int? DropDownWidth
		{
			get;
			set;
		}

		public int TabIndex
		{
			get;
			set;
		}

		public string ItemTemplateID
		{
			get;
			set;
		}

		public string HeaderTemplateID
		{
			get;
			set;
		}

		public string FooterTemplateID
		{
			get;
			set;
		}

		public string ToggleImageCssClass
		{
			get;
			set;
		}

		public MultiSelectClientSideEvents ClientSideEvents
		{
			get;
			set;
		}

		public Filter Filter
		{
			get;
			set;
		}

		public CoreMultiSelect()
		{
			ID = "";
			Width = 100;
			Height = 100;
			DropDownWidth = null;
			Items = new List<CoreListItem>();
			TabIndex = 0;
			ItemTemplateID = "";
			HeaderTemplateID = "";
			FooterTemplateID = "";
			ToggleImageCssClass = "";
			ClientSideEvents = new MultiSelectClientSideEvents();
			Filter = Filter.None;
		}

		internal List<Hashtable> SerializeItems(List<CoreListItem> items)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (CoreListItem item in items)
			{
				list.Add(item.ToHashtable());
			}
			return list;
		}

		public static List<CoreListItem> GetSelectedItems(string json)
		{
			return JsonConvert.DeserializeObject<List<CoreListItem>>(json);
		}
	}
}
