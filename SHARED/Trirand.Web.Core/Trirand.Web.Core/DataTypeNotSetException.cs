using System;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class DataTypeNotSetException : Exception
	{
		public DataTypeNotSetException(string message)
			: base(message)
		{
		}
	}
}
