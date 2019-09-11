namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class ViewRowDialogSettings
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

		public ViewRowDialogSettings()
		{
			int num3 = TopOffset = (LeftOffset = 0);
			num3 = (Width = (Height = 300));
			Modal = false;
			bool resizable = Draggable = true;
			Resizable = resizable;
			string text2 = CancelText = "";
			string text5 = Caption = (SubmitText = text2);
		}
	}
}
