namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class EditDialogSettings
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

		public bool Resizable
		{
			get;
			set;
		}

		public bool Draggable
		{
			get;
			set;
		}

		public string Caption
		{
			get;
			set;
		}

		public string SubmitText
		{
			get;
			set;
		}

		public string CancelText
		{
			get;
			set;
		}

		public string LoadingMessageText
		{
			get;
			set;
		}

		public bool CloseAfterEditing
		{
			get;
			set;
		}

		public bool ReloadAfterSubmit
		{
			get;
			set;
		}

		public EditDialogSettings()
		{
			int num3 = TopOffset = (LeftOffset = 0);
			num3 = (Width = (Height = 300));
			bool modal = CloseAfterEditing = false;
			Modal = modal;
			bool flag3 = ReloadAfterSubmit = true;
			modal = (Draggable = flag3);
			Resizable = modal;
			string text2 = LoadingMessageText = "";
			string text4 = CancelText = text2;
			string text7 = Caption = (SubmitText = text4);
		}
	}
}
