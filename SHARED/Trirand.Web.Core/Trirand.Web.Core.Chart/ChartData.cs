using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core.Chart
{
	public class ChartData
	{
		public string Title
		{
			get;
			set;
		}

		public string Ajax
		{
			get;
			set;
		}

		public IList<object> Raw
		{
			get;
			set;
		}

		public Dictionary<string, object> Data
		{
			get;
			set;
		}

		public IList<object> BeforeRaw
		{
			get;
			set;
		}

		public Dictionary<string, object> Fun
		{
			get;
			set;
		}

		public Dictionary<string, object> Fundata
		{
			get;
			set;
		}

		public bool IsSetOption
		{
			get;
			set;
		}

		public ChartOption Option
		{
			get;
			set;
		}

		public ChartData AddData(string name, object data)
		{
			if (Data == null)
			{
				Data = new Dictionary<string, object>();
			}
			Data.Add(name, data);
			return this;
		}

		public ChartData AddFunData(string name, object data)
		{
			if (Fundata == null)
			{
				Fundata = new Dictionary<string, object>();
			}
			Fundata.Add(name, data);
			return this;
		}

		public ChartData AddBeforeRaw(object raw)
		{
			if (BeforeRaw == null)
			{
				BeforeRaw = new List<object>();
			}
			BeforeRaw.Add(raw);
			return this;
		}

		public ChartData AddRaw(object raw)
		{
			if (Raw == null)
			{
				Raw = new List<object>();
			}
			Raw.Add(raw);
			return this;
		}

		public ChartData AddFun(string name, object fun)
		{
			if (Fun == null)
			{
				Fun = new Dictionary<string, object>();
			}
			Fun.Add(name, fun);
			return this;
		}
	}
}
