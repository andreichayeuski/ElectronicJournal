using System.Collections.Generic;
using Trirand.Web.Core.Trirand.Web.Core.Chart.bmap;
using Trirand.Web.Core.Trirand.Web.Core.Chart.ChartAxis;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class ChartOption
	{
		public object BackgroundColor
		{
			get;
			set;
		}

		public object Color
		{
			get;
			set;
		}

		public bool? RenderAsImage
		{
			get;
			set;
		}

		public bool? Calculable
		{
			get;
			set;
		}

		public bool? Animation
		{
			get;
			set;
		}

		public int? AnimationThreshold
		{
			get;
			set;
		}

		public int? AnimationDuration
		{
			get;
			set;
		}

		public string AnimationEasing
		{
			get;
			set;
		}

		public int? AnimationDelay
		{
			get;
			set;
		}

		public int? AnimationDurationUpdate
		{
			get;
			set;
		}

		public string AnimationEasingUpdate
		{
			get;
			set;
		}

		public object AnimationDelayUpdate
		{
			get;
			set;
		}

		public TimeLine Timeline
		{
			get;
			set;
		}

		public IList<Title> Title
		{
			get;
			set;
		}

		public ToolBox Toolbox
		{
			get;
			set;
		}

		public ToolTip Tooltip
		{
			get;
			set;
		}

		public Legend Legend
		{
			get;
			set;
		}

		public AxisPointer AxisPointer
		{
			get;
			set;
		}

		public IList<DataZoom> DataZoom
		{
			get;
			set;
		}

		public IList<Grid> Grid
		{
			get;
			set;
		}

		public object Polar
		{
			get;
			set;
		}

		public IList<Axis> XAxis
		{
			get;
			set;
		}

		public IList<Axis> YAxis
		{
			get;
			set;
		}

		public AngleAxis AngleAxis
		{
			get;
			set;
		}

		public object Series
		{
			get;
			set;
		}

		public ChartOption BaseOption
		{
			get;
			set;
		}

		public IList<ChartOption> Options
		{
			get;
			set;
		}

		public string NoDataEffect
		{
			get;
			set;
		}

		public IList<Radar> Radar
		{
			get;
			set;
		}

		public BMap Bmap
		{
			get;
			set;
		}

		public Geo Geo
		{
			get;
			set;
		}

		public Brush Brush
		{
			get;
			set;
		}

		public Parallel Parallel
		{
			get;
			set;
		}

		public IList<ParallelAxis> ParallelAxis
		{
			get;
			set;
		}

		public RadiusAxis RadiusAxis
		{
			get;
			set;
		}

		public object SingleAxis
		{
			get;
			set;
		}

		public VisualMap VisualMap
		{
			get;
			set;
		}

		public IList<Calendar> Calendar
		{
			get;
			set;
		}
	}
}
