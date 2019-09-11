using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class DataZoom : Basic<DataZoom>
	{
		public DataZoomType? Type
		{
			get;
			set;
		}

		public new OrientType? Orient
		{
			get;
			set;
		}

		public new int? Width
		{
			get;
			set;
		}

		public new int? Height
		{
			get;
			set;
		}

		public string FillerColor
		{
			get;
			set;
		}

		public string HandleIcon
		{
			get;
			set;
		}

		public string HandleColor
		{
			get;
			set;
		}

		public object HandleSize
		{
			get;
			set;
		}

		public object XAxisIndex
		{
			get;
			set;
		}

		public Normal HanleStyle
		{
			get;
			set;
		}

		public Normal DataBackground
		{
			get;
			set;
		}

		public object YAxisIndex
		{
			get;
			set;
		}

		public bool? Realtime
		{
			get;
			set;
		}

		public int? Start
		{
			get;
			set;
		}

		public string StartValue
		{
			get;
			set;
		}

		public int? End
		{
			get;
			set;
		}

		public bool? ZoomLock
		{
			get;
			set;
		}

		public string FilterMode
		{
			get;
			set;
		}
	}
}
