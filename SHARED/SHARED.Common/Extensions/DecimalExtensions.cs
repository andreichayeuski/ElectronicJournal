using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Extensions
{
    public static class DecimalExtensions
    {
        //public static decimal Round(this decimal val, int precision = 1)
        //{
        //    return Math.Round(val, precision, MidpointRounding.AwayFromZero);
        //}

        //public static decimal Round(this decimal? val, int precision = 1)
        //{
        //    if (val == null)
        //        return 0;
        //    return Math.Round((decimal)val, precision, MidpointRounding.AwayFromZero);
        //}

        public static decimal Divide(this decimal val, decimal divider)
        {
            return val/divider;
        }

        public static decimal Divide(this decimal? val, decimal divider)
        {
            if (val == null)
                return 0;

            return ((decimal)val) / divider;
        }
    }
}
