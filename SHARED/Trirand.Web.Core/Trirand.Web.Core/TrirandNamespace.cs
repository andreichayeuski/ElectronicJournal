using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Html;
using Trirand.Web.Core.Trirand.Web.Core.Chart;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class TrirandNamespace
	{
		public HtmlString CoreGrid(CoreGrid grid, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreGridRenderer coreGridRenderer = new CoreGridRenderer();
			grid.ID = id;
			return new HtmlString(coreGridRenderer.RenderHtml(grid));
		}

		public HtmlString CoreDatePicker(CoreDatePicker datePicker, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreDatePickerRenderer coreDatePickerRenderer = new CoreDatePickerRenderer();
			datePicker.ID = id;
			return new HtmlString(coreDatePickerRenderer.RenderHtml(datePicker));
		}

		public HtmlString CoreAutoComplete(CoreAutoComplete autoComplete, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreAutoCompleteRenderer coreAutoCompleteRenderer = new CoreAutoCompleteRenderer();
			autoComplete.ID = id;
			return new HtmlString(coreAutoCompleteRenderer.RenderHtml(autoComplete));
		}

		public HtmlString CoreTreeView(CoreTreeView tree, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreTreeViewRenderer coreTreeViewRenderer = new CoreTreeViewRenderer(tree);
			tree.ID = id;
			return new HtmlString(coreTreeViewRenderer.RenderHtml());
		}

		public HtmlString CoreDropDownList(CoreDropDownList dropDownList, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreDropDownListRenderer coreDropDownListRenderer = new CoreDropDownListRenderer(dropDownList);
			dropDownList.ID = id;
			return new HtmlString(coreDropDownListRenderer.RenderHtml());
		}

		public HtmlString CoreComboBox(CoreComboBox comboBox, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreComboBoxRenderer coreComboBoxRenderer = new CoreComboBoxRenderer(comboBox);
			comboBox.ID = id;
			return new HtmlString(coreComboBoxRenderer.RenderHtml());
		}

		public HtmlString CoreMultiSelect(CoreMultiSelect multiSelect, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreMultiSelectRenderer coreMultiSelectRenderer = new CoreMultiSelectRenderer(multiSelect);
			multiSelect.ID = id;
			return new HtmlString(coreMultiSelectRenderer.RenderHtml());
		}

		public HtmlString CoreChart(CoreChart chart, string id)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			CoreChartRenderer coreChartRenderer = new CoreChartRenderer(chart);
			chart.ID = id;
			return new HtmlString(coreChartRenderer.RenderHtml());
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		private new bool Equals(object value)
		{
			return base.Equals(value);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		private new int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		private new Type GetType()
		{
			return base.GetType();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		private new string ToString()
		{
			return base.ToString();
		}
	}
}
