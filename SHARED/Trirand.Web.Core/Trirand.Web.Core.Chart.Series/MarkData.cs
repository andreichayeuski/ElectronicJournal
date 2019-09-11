using Trirand.Web.Core.Trirand.Web.Core.Chart.Data;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class MarkData : BasicData<MarkData>
	{
		public MarkData()
		{
		}

		public MarkData(string name, MarkType type)
			: this()
		{
			base.Name = name;
			base.Type = type;
		}

		public MarkData(MarkType type)
			: this()
		{
			base.Type = type;
		}

		public MarkData(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
