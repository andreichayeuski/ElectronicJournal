using System;
using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreDatePicker
	{
		public bool Enabled
		{
			get;
			set;
		}

		public string AltField
		{
			get;
			set;
		}

		public string AltFormat
		{
			get;
			set;
		}

		public string AppendText
		{
			get;
			set;
		}

		public bool AutoSize
		{
			get;
			set;
		}

		public string ButtonImage
		{
			get;
			set;
		}

		public bool ButtonImageOnly
		{
			get;
			set;
		}

		public string ButtonText
		{
			get;
			set;
		}

		public bool ChangeMonth
		{
			get;
			set;
		}

		public bool ChangeYear
		{
			get;
			set;
		}

		public string CloseText
		{
			get;
			set;
		}

		public bool ConstrainInput
		{
			get;
			set;
		}

		public string CurrentText
		{
			get;
			set;
		}

		public string DateFormat
		{
			get;
			set;
		}

		public List<string> DayNames
		{
			get;
			set;
		}

		public List<string> DayNamesMin
		{
			get;
			set;
		}

		public List<string> DayNamesShort
		{
			get;
			set;
		}

		public DatePickerDisplayMode DisplayMode
		{
			get;
			set;
		}

		public DateTime? DefaultDate
		{
			get;
			set;
		}

		public int Duration
		{
			get;
			set;
		}

		public int FirstDay
		{
			get;
			set;
		}

		public bool GotoCurrent
		{
			get;
			set;
		}

		public bool HideIfNoPrevNext
		{
			get;
			set;
		}

		public string ID
		{
			get;
			set;
		}

		public bool IsRTL
		{
			get;
			set;
		}

		public DateTime? MaxDate
		{
			get;
			set;
		}

		public DateTime? MinDate
		{
			get;
			set;
		}

		public List<string> MonthNames
		{
			get;
			set;
		}

		public List<string> MonthNamesShort
		{
			get;
			set;
		}

		public bool NavigationAsDateFormat
		{
			get;
			set;
		}

		public string NextText
		{
			get;
			set;
		}

		public string PrevText
		{
			get;
			set;
		}

		public bool ShowButtonPanel
		{
			get;
			set;
		}

		public bool ShowMonthAfterYear
		{
			get;
			set;
		}

		public ShowOn ShowOn
		{
			get;
			set;
		}

		public CoreDatePicker()
		{
			Enabled = true;
			AltField = "";
			AltFormat = "";
			AppendText = "";
			AutoSize = false;
			ButtonImage = "";
			ButtonImageOnly = false;
			ButtonText = "...";
			ChangeMonth = false;
			ChangeYear = false;
			CloseText = "Done";
			ConstrainInput = true;
			CurrentText = "Today";
			DateFormat = "yyyy/MM/dd";
			DayNames = new List<string>();
			DayNamesMin = new List<string>();
			DayNamesShort = new List<string>();
			DisplayMode = DatePickerDisplayMode.Standalone;
			DefaultDate = null;
			Duration = 500;
			FirstDay = 0;
			GotoCurrent = false;
			HideIfNoPrevNext = false;
			IsRTL = false;
			MaxDate = null;
			MinDate = null;
			MonthNames = new List<string>();
			MonthNamesShort = new List<string>();
			NavigationAsDateFormat = false;
			NextText = "Next";
			PrevText = "Prev";
			ShowButtonPanel = false;
			ShowMonthAfterYear = false;
			ShowOn = ShowOn.Focus;
		}
	}
}
