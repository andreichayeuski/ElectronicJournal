using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsDefined(this Enum @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var result = Enum.IsDefined(@this.GetType(), @this);

            return result;
        }

        public static string GetDescription(this Enum e)
        {
            var type = e.GetType();

            var memberInfos = type.GetMember(e.ToString());

            if (memberInfos != null && memberInfos.Length > 0)
            {
                var attributes = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute),
                                                                false);
                if (attributes != null && attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }

            return string.Empty;
            //throw new ArgumentException("Enum " + e.ToString() + " has no Description defined!");
        }

        public static IList ToList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(value);
            }

            return list;
        }

        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<T>().SingleOrDefault();
        }
    }
}
