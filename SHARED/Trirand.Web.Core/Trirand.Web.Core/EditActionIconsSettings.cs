namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class EditActionIconsSettings
	{
		public bool ShowEditIcon
		{
			get;
			set;
		}

		public bool ShowDeleteIcon
		{
			get;
			set;
		}

		public bool SaveOnEnterKeyPress
		{
			get;
			set;
		}

		public EditActionIconsSettings()
		{
			ShowEditIcon = true;
			ShowDeleteIcon = true;
			SaveOnEnterKeyPress = false;
		}
	}
}
