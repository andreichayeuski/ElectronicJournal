namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class TreeGridSettings
	{
		public bool Enabled
		{
			get;
			set;
		}

		public string CollapsedIcon
		{
			get;
			set;
		}

		public string ExpandedIcon
		{
			get;
			set;
		}

		public string LeafIcon
		{
			get;
			set;
		}

		public TreeGridSettings()
		{
			Enabled = false;
			CollapsedIcon = "";
			ExpandedIcon = "";
			LeafIcon = "";
		}
	}
}
