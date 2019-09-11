using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class PivotSettings
	{
		public bool FrozenStaticCols;

		public List<PivotDimension> XDimension
		{
			get;
			set;
		}

		public List<PivotDimension> YDimension
		{
			get;
			set;
		}

		public List<PivotAggregate> Aggregates
		{
			get;
			set;
		}

		public bool RowTotals
		{
			get;
			set;
		}

		public string RowTotalsText
		{
			get;
			set;
		}

		public bool ColTotals
		{
			get;
			set;
		}

		public bool GroupSummary
		{
			get;
			set;
		}

		public GroupSummaryPosition GroupSummaryPosition
		{
			get;
			set;
		}

		public PivotSettings()
		{
			XDimension = new List<PivotDimension>();
			YDimension = new List<PivotDimension>();
			Aggregates = new List<PivotAggregate>();
			FrozenStaticCols = false;
			RowTotals = false;
			RowTotalsText = "Total";
			ColTotals = false;
			GroupSummary = true;
			GroupSummaryPosition = GroupSummaryPosition.Header;
		}

		internal bool IsPivotEnabled()
		{
			return XDimension.Count > 0;
		}

		internal string ToJSON()
		{
			return JsonConvert.SerializeObject((object)ToDictionary());
		}

		internal Hashtable ToDictionary()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("xDimension", SerializePivotDimension(XDimension));
			hashtable.Add("yDimension", SerializePivotDimension(YDimension));
			hashtable.Add("aggregates", SerializePivotAggregate(Aggregates));
			if (FrozenStaticCols)
			{
				hashtable.Add("frozenStaticCols", true);
			}
			if (RowTotals)
			{
				hashtable.Add("rowTotals", true);
			}
			if (ColTotals)
			{
				hashtable.Add("colTotals", true);
			}
			if (RowTotalsText != "Total")
			{
				hashtable.Add("rowTotalsText", RowTotalsText);
			}
			if (!GroupSummary)
			{
				hashtable.Add("groupSummary", false);
			}
			if (GroupSummaryPosition != 0)
			{
				hashtable.Add("groupSummaryPos", GroupSummaryPosition.ToString().ToLower());
			}
			return hashtable;
		}

		internal List<Hashtable> SerializePivotDimension(List<PivotDimension> dimensions)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (PivotDimension dimension in dimensions)
			{
				list.Add(dimension.ToHashtable());
			}
			return list;
		}

		internal List<Hashtable> SerializePivotAggregate(List<PivotAggregate> aggregates)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (PivotAggregate aggregate in aggregates)
			{
				list.Add(aggregate.ToHashtable());
			}
			return list;
		}
	}
}
