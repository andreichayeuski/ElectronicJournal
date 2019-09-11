namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class AppearanceSettings
	{
		public bool ShowRowNumbers
		{
			get;
			set;
		}

		public int RowNumbersColumnWidth
		{
			get;
			set;
		}

		public bool AlternateRowBackground
		{
			get;
			set;
		}

		public bool HighlightRowsOnHover
		{
			get;
			set;
		}

		public string Caption
		{
			get;
			set;
		}

		public bool HiddenGrid
		{
			get;
			set;
		}

		public bool ShowHideGridCaptionButton
		{
			get;
			set;
		}

		public int ScrollBarOffset
		{
			get;
			set;
		}

		public bool RightToLeft
		{
			get;
			set;
		}

		public bool ShowFooter
		{
			get;
			set;
		}

		public bool ShrinkToFit
		{
			get;
			set;
		}

		public AppearanceSettings()
		{
			bool flag2 = ShowFooter = false;
			bool flag4 = RightToLeft = flag2;
			bool flag6 = HighlightRowsOnHover = flag4;
			bool showRowNumbers = AlternateRowBackground = flag6;
			ShowRowNumbers = showRowNumbers;
			RowNumbersColumnWidth = 25;
			Caption = "";
			ScrollBarOffset = 18;
			ShrinkToFit = true;
			HiddenGrid = false;
			ShowHideGridCaptionButton = true;
		}
	}
}
