using System.Collections;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreListItem
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

		public NameValueCollection Options
		{
			get;
			set;
		}

		public string ItemTemplateID
		{
			get;
			set;
		}

		public CoreListItem()
		{
			Text = "";
			Value = "";
			Selected = false;
			Enabled = true;
			Url = "";
			ImageUrl = "";
			Options = new NameValueCollection();
			ItemTemplateID = "";
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
			if (!Enabled)
			{
				hashtable.Add("enabled", false);
			}
			if (Selected)
			{
				hashtable.Add("selected", true);
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
			if (!string.IsNullOrEmpty(ItemTemplateID))
			{
				hashtable.Add("itemTemplateID", ItemTemplateID);
			}
			string[] allKeys = Options.AllKeys;
			foreach (string text in allKeys)
			{
				string text2 = Options[text];
				if (text2 != null)
				{
					hashtable.Add(text, text2);
				}
			}
			return hashtable;
		}
	}
}
