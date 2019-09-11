using Trirand.Web.Core.Trirand.Web.Core.Chart.Style;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class Title : Basic<Title>
	{
		public string Id
		{
			get;
			set;
		}

		public object Text
		{
			get;
			set;
		}

		public string Link
		{
			get;
			set;
		}

		public TargetType? Target
		{
			get;
			set;
		}

		public string Subtext
		{
			get;
			set;
		}

		public string Sublink
		{
			get;
			set;
		}

		public TargetType? SubTarget
		{
			get;
			set;
		}

		public HorizontalType? TextAlign
		{
			get;
			set;
		}

		public HorizontalType? Align
		{
			get;
			set;
		}

		public new int? ItemGap
		{
			get;
			set;
		}

		public TextStyle TextStyle
		{
			get;
			set;
		}

		public TextStyle SubTextStyle
		{
			get;
			set;
		}
	}
}
