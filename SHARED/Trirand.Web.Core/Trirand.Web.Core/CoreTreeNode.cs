using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreTreeNode
	{
		public string Text
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		public List<CoreTreeNode> Nodes
		{
			get;
			set;
		}

		public bool Expanded
		{
			get;
			set;
		}

		public bool Enabled
		{
			get;
			set;
		}

		public bool Selected
		{
			get;
			set;
		}

		public bool Checked
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		public string ImageUrl
		{
			get;
			set;
		}

		public string ExpandedImageUrl
		{
			get;
			set;
		}

		public bool LoadOnDemand
		{
			get;
			set;
		}

		public Dictionary<string, string> Options
		{
			get;
			set;
		}

		public string NodeTemplateID
		{
			get;
			set;
		}

		public CoreTreeNode()
		{
			Text = "";
			Value = "";
			Nodes = new List<CoreTreeNode>();
			Selected = false;
			Expanded = false;
			Enabled = true;
			Checked = false;
			Url = "";
			ImageUrl = "";
			ExpandedImageUrl = "";
			LoadOnDemand = false;
			Options = new Dictionary<string, string>();
		}

		public string ToJSON()
		{
			return JsonConvert.SerializeObject((object)ToHashtable());
		}

		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(Text))
			{
				hashtable.Add("text", Text);
			}
			if (!string.IsNullOrEmpty(Value))
			{
				hashtable.Add("value", Value);
			}
			if (Expanded)
			{
				hashtable.Add("expanded", true);
			}
			if (!Enabled)
			{
				hashtable.Add("enabled", false);
			}
			if (Selected)
			{
				hashtable.Add("selected", true);
			}
			if (Checked)
			{
				hashtable.Add("checked", true);
			}
			if (LoadOnDemand)
			{
				hashtable.Add("loadOnDemand", true);
			}
			if (!string.IsNullOrEmpty(Url))
			{
				hashtable.Add("url", Url);
			}
			if (!string.IsNullOrEmpty(ImageUrl))
			{
				hashtable.Add("imageUrl", ImageUrl);
			}
			if (!string.IsNullOrEmpty(ExpandedImageUrl))
			{
				hashtable.Add("expandedImageUrl", ExpandedImageUrl);
			}
			if (!string.IsNullOrEmpty(NodeTemplateID))
			{
				hashtable.Add("nodeTemplateID", NodeTemplateID);
			}
			List<Hashtable> list = new List<Hashtable>();
			foreach (CoreTreeNode node in Nodes)
			{
				list.Add(node.ToHashtable());
			}
			if (list.Count > 0)
			{
				hashtable.Add("nodes", list);
			}
			foreach (string key in Options.Keys)
			{
				string text = Options[key];
				if (text != null)
				{
					hashtable.Add(key, text);
				}
			}
			return hashtable;
		}
	}
}
