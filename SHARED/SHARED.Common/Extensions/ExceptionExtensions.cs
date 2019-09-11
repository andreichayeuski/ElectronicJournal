using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetInnerExceptionTrace(this Exception exception)
        {
            StringBuilder sb=new StringBuilder();
            while (exception.InnerException!=null)
            {
                sb.AppendFormat("{0}{1}", exception.InnerException, Environment.NewLine);
                exception = exception.InnerException;
            }
            return sb.ToString();
        }
    }
}
