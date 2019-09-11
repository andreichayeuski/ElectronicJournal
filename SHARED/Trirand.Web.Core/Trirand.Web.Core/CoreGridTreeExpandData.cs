namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreGridTreeExpandData
	{
		public int ParentLevel
		{
			get;
			set;
		}

		public string ParentID
		{
			get;
			set;
		}

		public CoreGridTreeExpandData()
		{
			ParentLevel = -1;
			ParentID = null;
		}
	}
}
