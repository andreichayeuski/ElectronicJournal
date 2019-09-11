using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public sealed class GroupSettings
	{
		public List<GroupField> GroupFields
		{
			get;
			set;
		}

		public bool CollapseGroups
		{
			get;
			set;
		}

		public GroupSettings()
		{
			CollapseGroups = false;
			GroupFields = new List<GroupField>();
		}

		internal string ToJSON()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (GroupFields.Count > 0)
			{
				stringBuilder.Append(",grouping:true");
				stringBuilder.Append(",groupingView: {");
				stringBuilder.AppendFormat("groupField: {0}", GetDataFields());
				stringBuilder.AppendFormat(",groupColumnShow: {0}", GetGroupColumnShow());
				stringBuilder.AppendFormat(",groupText: {0}", GetHeaderText());
				stringBuilder.AppendFormat(",groupOrder: {0}", GetGroupSortDirection());
				stringBuilder.AppendFormat(",groupSummary: {0}", GetGroupShowGroupSummary());
				stringBuilder.AppendFormat(",groupCollapse: {0}", CollapseGroups.ToString().ToLower());
				stringBuilder.AppendFormat(",groupDataSorted: true");
				stringBuilder.Append("}");
			}
			return stringBuilder.ToString();
		}

		private string GetDataFields()
		{
			List<string> list = new List<string>();
			foreach (GroupField groupField in GroupFields)
			{
				list.Add(groupField.DataField);
			}
			return JsonConvert.SerializeObject((object)list);
		}

		private string GetGroupColumnShow()
		{
			List<bool> list = new List<bool>();
			foreach (GroupField groupField in GroupFields)
			{
				list.Add(groupField.ShowGroupColumn);
			}
			return JsonConvert.SerializeObject((object)list);
		}

		private string GetHeaderText()
		{
			List<string> list = new List<string>();
			foreach (GroupField groupField in GroupFields)
			{
				list.Add(groupField.HeaderText);
			}
			return JsonConvert.SerializeObject((object)list);
		}

		private string GetGroupSortDirection()
		{
			List<string> list = new List<string>();
			foreach (GroupField groupField in GroupFields)
			{
				list.Add(groupField.GroupSortDirection.ToString().ToLower());
			}
			return JsonConvert.SerializeObject((object)list);
		}

		private string GetGroupShowGroupSummary()
		{
			List<bool> list = new List<bool>();
			foreach (GroupField groupField in GroupFields)
			{
				list.Add(groupField.ShowGroupSummary);
			}
			return JsonConvert.SerializeObject((object)list);
		}
	}
}
