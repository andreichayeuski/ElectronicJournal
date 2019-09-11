using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class EventData
	{
		public string Name
		{
			get;
			set;
		}

		public int? Weight
		{
			get;
			set;
		}

		public IList<EventEvolution> Evolution
		{
			get;
			set;
		}

		public EventData()
		{
		}

		public EventData(string name)
		{
			Name = name;
		}
	}
}
