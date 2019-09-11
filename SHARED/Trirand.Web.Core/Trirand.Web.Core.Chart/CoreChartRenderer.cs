using System;
using System.Collections.Generic;
using System.Text;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Series;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	internal class CoreChartRenderer
	{
		private CoreChart _chart;

		public CoreChartRenderer(CoreChart chart)
		{
			_chart = chart;
		}

		public string RenderHtml()
		{
			//if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
			//{
			//	return "This is a trial version of CoreSuite for ASP.NET Core which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
			//}
			Guard.IsNotNullOrEmpty(_chart.ID, "ID", "You need to set ID for this CoreChart instance.");
			return GetStartupJavascript();
		}

		public string GetStartupJavascript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"<div id='{_chart.ID}' style='height:{_chart.Height};width:{_chart.Width}'></div>");
			stringBuilder.Append("<script type='text/javascript'>");
			ProcessBoxplot(stringBuilder);
			stringBuilder.Append($"var dom = document.getElementById('{_chart.ID}');");
			stringBuilder.Append($"var {_chart.ID} = echarts.init(dom);");
			string arg = JsonTools.ObjectToJson2(_chart);
			stringBuilder.Append($"{_chart.ID}.setOption({arg}, true);");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		private void ProcessBoxplot(StringBuilder sb)
		{
			List<object> list = default(List<object>);
			if (_chart.IsBoxPlot() && (list = (_chart.Series as List<object>)) != null)
			{
				foreach (object item in list)
				{
					if (item is Boxplot)
					{
						Boxplot boxplot = item as Boxplot;
						if (string.IsNullOrEmpty(boxplot.DataClientID))
						{
							throw new Exception("Must set DataClientID for Boxplot series.");
						}
						sb.Append($"var {boxplot.DataClientID} =  echarts.dataTool.prepareBoxplotData({_chart.GetBoxPlotSerieData(item)});");
					}
				}
			}
		}
	}
}
