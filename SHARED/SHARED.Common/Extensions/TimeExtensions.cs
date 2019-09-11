using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED.Common.Utils;

namespace SHARED.Common.Extensions
{
    public static class TimeExtensions
    {
        public static bool IsPast(this Time @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var result = @this < Time.Now;
            return result;
        }

        public static TimeSpan DueTime(this Time @this)
        {
            var result = @this - Time.Now;

            if (@this.IsPast())
            {
                result = result + TimeSpan.FromHours(24);
            }

            return result;
        }
    }
}
