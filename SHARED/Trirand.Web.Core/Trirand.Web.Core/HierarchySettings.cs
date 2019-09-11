using System.Collections;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class HierarchySettings
	{
		public HierarchyMode HierarchyMode
		{
			get;
			set;
		}

		public string PlusIcon
		{
			get;
			set;
		}

		public string MinusIcon
		{
			get;
			set;
		}

		public string OpenIcon
		{
			get;
			set;
		}

		public bool ExpandOnLoad
		{
			get;
			set;
		}

		public bool SelectOnExpand
		{
			get;
			set;
		}

		public bool ReloadOnExpand
		{
			get;
			set;
		}

		public HierarchySettings()
		{
			HierarchyMode = HierarchyMode.None;
			PlusIcon = "ui-icon-plus";
			MinusIcon = "ui-icon-minus";
			OpenIcon = "ui-icon-carat-1-sw";
			ExpandOnLoad = false;
			SelectOnExpand = false;
			ReloadOnExpand = true;
		}

		internal string ToJSON()
		{
			return JsonConvert.SerializeObject((object)ToHashtable());
		}

		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (PlusIcon != null && PlusIcon != "ui-icon-plus")
			{
				hashtable.Add("plusicon", PlusIcon);
			}
			if (MinusIcon != null && MinusIcon != "ui-icon-minus")
			{
				hashtable.Add("minusicon", MinusIcon);
			}
			if (OpenIcon != null && OpenIcon != "ui-icon-carat-1-sw")
			{
				hashtable.Add("openicon", OpenIcon);
			}
			if (ExpandOnLoad)
			{
				hashtable.Add("expandOnLoad", true);
			}
			if (SelectOnExpand)
			{
				hashtable.Add("selectOnExpand", true);
			}
			if (!ReloadOnExpand)
			{
				hashtable.Add("reloadOnExpand", false);
			}
			return hashtable;
		}
	}
}
