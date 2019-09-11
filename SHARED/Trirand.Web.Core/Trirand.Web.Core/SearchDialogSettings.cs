namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class SearchDialogSettings
	{
		public int TopOffset
		{
			get;
			set;
		}

		public int LeftOffset
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public bool Modal
		{
			get;
			set;
		}

		public bool Draggable
		{
			get;
			set;
		}

		public string FindButtonText
		{
			get;
			set;
		}

		public string ResetButtonText
		{
			get;
			set;
		}

		public bool MultipleSearch
		{
			get;
			set;
		}

		public bool ValidateInput
		{
			get;
			set;
		}

		public bool Resizable
		{
			get;
			set;
		}

		public SearchDialogSettings()
		{
			int num3 = TopOffset = (LeftOffset = 0);
			num3 = (Width = (Height = 300));
			bool flag2 = ValidateInput = false;
			bool modal = MultipleSearch = flag2;
			Modal = modal;
			Draggable = true;
			string text3 = FindButtonText = (ResetButtonText = "");
		}
	}
}
