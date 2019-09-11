using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SHARED.Common.Extensions
{
    public static class AttributeExtensions
    {
        public static MemberInfo GetProperty<TModel, TProperty>(this TModel instance, Expression<Func<TModel, TProperty>> propertySelector)
        {
            return ((MemberExpression) propertySelector.Body).Member;
        }

        public static MethodInfo GetMethod<T>(this T instance,
            Expression<Action<T>> methodSelector)
        {
            return ((MethodCallExpression)methodSelector.Body).Method;
        }

        public static bool HasAttribute<TAttribute>(
            this MemberInfo member)
            where TAttribute : Attribute
        {
            return GetAttributes<TAttribute>(member).Length > 0;
        }

        public static TAttribute[] GetAttributes<TAttribute>(
            this MemberInfo member)
            where TAttribute : Attribute
        {
            var attributes =
                member.GetCustomAttributes(typeof(TAttribute), true);

            return (TAttribute[])attributes;
        }

        //public static string GetDisplayName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
        //{

        //    Type type = typeof(TModel);

        //    MemberExpression memberExpression = (MemberExpression)expression.Body;
        //    string propertyName = ((memberExpression.Member is PropertyInfo) ? memberExpression.Member.Name : null);

        //     First look into attributes on a type and it's parents
        //    DisplayAttribute attr;
        //    attr = (DisplayAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

        //     Look for [MetadataType] attribute in type hierarchy
        //     http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
        //    if (attr == null)
        //    {
        //        MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
        //        if (metadataType != null)
        //        {
        //            var property = metadataType.MetadataClassType.GetProperty(propertyName);
        //            if (property != null)
        //            {
        //                attr = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
        //            }
        //        }
        //    }
        //    return (attr != null) ? attr.Name : String.Empty;


        //}

    }
}
