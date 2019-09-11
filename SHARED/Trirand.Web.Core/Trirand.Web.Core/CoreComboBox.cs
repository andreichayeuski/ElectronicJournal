using System.Collections;
using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreComboBox
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

		public ComboBoxClientSideEvents ClientSideEvents
		{
			get;
			set;
		}

		public Filter Filter
		{
			get;
			set;
		}

		public CoreComboBox()
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
			ClientSideEvents = new ComboBoxClientSideEvents();
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
	}
}
