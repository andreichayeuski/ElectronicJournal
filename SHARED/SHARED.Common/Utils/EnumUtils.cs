using System;
using System.Linq;

namespace SHARED.Common.Utils
{
    public static class EnumUtils
    {
        public static TEnum Parse<TEnum>(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException();
            }

            var enumType = typeof(TEnum);

            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("Operation is supported only for Enum.");
            }

            var result = (TEnum)Enum.Parse(enumType, str);

            return result;
        }

        public static TEnum[] GetValues<TEnum>()
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("Operation is supported only for Enum.");
            }

            var values = Enum.GetValues(enumType)
                             .Cast<TEnum>()
                             .ToArray();

            return values;
        }
    }
}
