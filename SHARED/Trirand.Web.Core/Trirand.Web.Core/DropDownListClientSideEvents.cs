namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class DropDownListClientSideEvents
	{
		public string Show
		{
			get;
			set;
		}

		public string Hide
		{
			get;
			set;
		}

		public string Select
		{
			get;
			set;
		}

		public string MouseOver
		{
			get;
			set;
		}

		public string MouseOut
		{
			get;
			set;
		}

		public string Initialized
		{
			get;
			set;
		}

		public string KeyDown
		{
			get;
			set;
		}

		public DropDownListClientSideEvents()
		{
			Show = "";
			Hide = "";
			Select = "";
			MouseOver = "";
			MouseOut = "";
			Initialized = "";
			KeyDown = "";
		}
	}
}
