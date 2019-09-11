using System.Collections;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreGridHeaderGroup
	{
		public string StartColumnName
		{
			get;
			set;
		}

		public int NumberOfColumns
		{
			get;
			set;
		}

		public string TitleText
		{
			get;
			set;
		}

		public CoreGridHeaderGroup()
		{
			StartColumnName = "";
			NumberOfColumns = 1;
			TitleText = "";
		}

		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(StartColumnName))
			{
				hashtable["startColumnName"] = StartColumnName;
			}
			hashtable["numberOfColumns"] = NumberOfColumns;
			if (!string.IsNullOrEmpty(TitleText))
			{
				hashtable["titleText"] = TitleText;
			}
			return hashtable;
		}
	}
}
