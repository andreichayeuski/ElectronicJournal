using System.Collections;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class PivotAggregate
	{
		public string Member
		{
			get;
			set;
		}

		public string Label
		{
			get;
			set;
		}

		public PivotAggregator Aggregator
		{
			get;
			set;
		}

		public int? Width
		{
			get;
			set;
		}

		public GroupSummaryType GroupSummaryType
		{
			get;
			set;
		}

		public PivotFormatter Formatter
		{
			get;
			set;
		}

		public TextAlign Align
		{
			get;
			set;
		}

		public PivotAggregate()
		{
			Member = "";
			Label = "";
			Aggregator = PivotAggregator.None;
			Width = null;
			GroupSummaryType = GroupSummaryType.None;
			Formatter = PivotFormatter.None;
			Align = TextAlign.Left;
		}

		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(Member))
			{
				hashtable.Add("member", Member);
			}
			if (!string.IsNullOrEmpty(Label))
			{
				hashtable.Add("label", Label);
			}
			if (Aggregator != 0)
			{
				hashtable.Add("aggregator", Aggregator.ToString().ToLower());
			}
			if (Width.HasValue)
			{
				hashtable.Add("width", Width);
			}
			if (GroupSummaryType != 0)
			{
				hashtable.Add("summaryType", GroupSummaryType.ToString().ToLower());
			}
			if (Formatter != 0)
			{
				hashtable.Add("formatter", Formatter.ToString().ToLower());
			}
			if (Align != 0)
			{
				hashtable.Add("align", Align.ToString().ToLower());
			}
			return hashtable;
		}
	}
}
