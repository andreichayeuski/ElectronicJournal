using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SHARED.Web.Extensions
{
    public static class RouteValueDictionaryExtensions
    {
        public static RouteValueDictionary ToRouteValueDictionaryWithCollection(this RouteValueDictionary routeValues)
        {
            var newRouteValues = new RouteValueDictionary();

            foreach (var key in routeValues.Keys)
            {
                object value = routeValues[key];

                if (value is IEnumerable && !(value is string))
                {
                    int index = 0;
                    foreach (object val in (IEnumerable)value)
                    {
                        if (val != null)
                        {
                            if (val is string || val.GetType().IsPrimitive || val.GetType().IsEnum)
                            {
                                newRouteValues.Add(
                                    String.Format("{0}[{1}]", key, index),
                                    val);
                            }
                            else
                            {
                                PropertyInfo[] properties = val.GetType().GetProperties();
                                foreach (PropertyInfo propInfo in properties)
                                {
                                    newRouteValues.Add(
                                        String.Format("{0}[{1}].{2}", key, index, propInfo.Name),
                                        propInfo.GetValue(val));
                                }
                            }

                            index++;
                        }
                    }  
                }
                else if (value is DateTime)
                {
                    newRouteValues.Add(key, ((DateTime)value).ToString("yyyy-MM-dd"));
                }
                else // остальные типы
                {
                    newRouteValues.Add(key, value);
                }
            }

            return newRouteValues;
        }
    }
}
