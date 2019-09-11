using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Trirand.Web.Core.Trirand.Web.Core.Chart.Series;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class CoreChart : ChartOption
	{
		[JsonIgnore]
		public string ID
		{
			get;
			set;
		} = "";


		[JsonIgnore]
		public string Width
		{
			get;
			set;
		} = "500px";


		[JsonIgnore]
		public string Height
		{
			get;
			set;
		} = "500px";


		internal bool IsBoxPlot()
		{
			List<object> list;
			if ((list = (base.Series as List<object>)) != null)
			{
				foreach (object item in list)
				{
					if (item is Boxplot)
					{
						return true;
					}
				}
			}
			return false;
		}

		internal string GetBoxPlotSerieData(object o)
		{
			string str = "[";
			Boxplot boxplot = o as Boxplot;
			List<List<double>> list = boxplot.BoxPlotData as List<List<double>>;
			if (list == null)
			{
				throw new Exception("Boxplot Series has no BoxPlotData defined or the type of the data is not of the required type - List<List<double>>");
			}
			for (int i = 0; i < list.Count; i++)
			{
				List<double> list2 = list[i];
				str += "[";
				for (int j = 0; j < list2.Count; j++)
				{
					str += list2[j].ToString();
					if (j < list2.Count - 1)
					{
						str += ",";
					}
				}
				str += "]";
				if (i < list.Count - 1)
				{
					str += ",";
				}
			}
			str += "]";
			if (boxplot.Orientation == OrientType.Vertical)
			{
				str += ", {layout: 'vertical'}";
			}
			return str;
		}
	}
}
