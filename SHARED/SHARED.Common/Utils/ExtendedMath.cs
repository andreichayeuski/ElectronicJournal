using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Utils
{
    public class ExtendedMath
    {
        // возведения в дробную степень в стандартном классе работает некорректно
        // https://stackoverflow.com/questions/14367448/c-sharp-math-pow-is-broken
        public static double Pow(double expBase, double power)
        {
            bool sign = (expBase < 0);
            if (sign && HasEvenDenominator(power))
                return double.NaN;  //sqrt(-1) = i
            else
            {
                if (sign && HasOddDenominator(power))
                    return -1 * Math.Pow(Math.Abs(expBase), power);
                else
                    return Math.Pow(expBase, power);
            }
        }

        public static decimal Pow(decimal expBase, decimal power)
        {
            bool sign = (expBase < 0);
            if (sign && HasEvenDenominator(Convert.ToDouble(power)))
                return decimal.MinusOne;  //sqrt(-1) = i
            else
            {
                if (sign && HasOddDenominator(Convert.ToDouble(power)))
                    return -1 * Convert.ToDecimal(Math.Pow(Math.Abs(Convert.ToDouble(expBase)), Convert.ToDouble(power)));
                else
                    return Convert.ToDecimal(Math.Pow(Convert.ToDouble(expBase), Convert.ToDouble(power)));
            }
        }

        private static bool HasEvenDenominator(double input)
        {
            if (input == 0)
                return false;
            else if (input % 1 == 0)
                return false;

            double inverse = 1 / input;
            if (inverse % 2 < double.Epsilon)
                return true;
            else
                return false;
        }

        private static bool HasOddDenominator(double input)
        {
            if (input == 0)
                return false;
            else if (input % 1 == 0)
                return false;

            double inverse = 1 / input;
            if ((inverse + 1) % 2 < double.Epsilon)
                return true;
            else
                return false;
        }

    }


}
