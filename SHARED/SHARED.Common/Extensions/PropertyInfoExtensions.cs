using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SHARED.Common.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static PropertyInfo[] GetPropertyInfos<TModel>(this TModel model) where TModel:class 
        {
            return model.GetType().GetProperties();
        }

        public static TValue GetAttributValue<TAttribute, TValue>(this PropertyInfo prop, Func<TAttribute, TValue> value) where TAttribute : Attribute
        {
            var att = prop.GetCustomAttributes(
                typeof(TAttribute), true
                ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return value(att);
            }
            return default(TValue);
        }

        public static IEnumerable<PropertyInfo>  GetPropertyWithAttribute<TModel,TAttribute>(this TModel model) where TModel : class
        {
            return model.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(TAttribute)));
        }
    }
}
