namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class DeleteDialogSettings
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

		public bool Resizable
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

		public string DeleteMessage
		{
			get;
			set;
		}

		public string Caption
		{
			get;
			set;
		}

		public bool ReloadAfterSubmit
		{
			get;
			set;
		}

		public DeleteDialogSettings()
		{
			int num3 = TopOffset = (LeftOffset = 0);
			num3 = (Width = (Height = 300));
			Modal = false;
			bool flag2 = ReloadAfterSubmit = true;
			bool resizable = Draggable = flag2;
			Resizable = resizable;
			string text2 = LoadingMessageText = "";
			string text4 = CancelText = text2;
			string text7 = Caption = (SubmitText = text4);
		}
	}
}
