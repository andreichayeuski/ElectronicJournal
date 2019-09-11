using System.Collections;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class PivotDimension
	{
		private Hashtable _functionsHash;

		public string DataName
		{
			get;
			set;
		}

		public string Converter
		{
			get;
			set;
		}

		public PivotDimension()
		{
			DataName = "";
			Converter = "";
		}

		public PivotDimension(CoreGrid grid)
			: this()
		{
			_functionsHash = grid.FunctionsHash;
		}

		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			hashtable.AddIfNotEmpty("dataName", DataName);
			hashtable.AddIfNotEmpty("converter", Converter, _functionsHash);
			return hashtable;
		}
	}
}
