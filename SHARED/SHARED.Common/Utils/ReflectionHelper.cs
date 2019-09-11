using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SHARED.Common.Utils
{
    public class ReflectionHelper
    {
        public static List<PropertyInfoModel> GetPropertiesList<T>() where T : class
        {
            var result = new List<PropertyInfoModel>();

            var propertiesList = typeof(T).GetProperties();

            foreach (var property in propertiesList)
                result.AddRange(GetPropertiesListRecursive(property, "Базовые свойства", ""));
            return result;
        }

        private static List<PropertyInfoModel> GetPropertiesListRecursive(PropertyInfo property, string groupName,
            string listPropertyName)
        {
            var result = new List<PropertyInfoModel>();

            if (property.CustomAttributes.Any(c => c.AttributeType == typeof(DisplayAttribute)) &&
                (!property.PropertyType.IsGenericType ||
                 property.PropertyType.IsGenericType &&
                 property.PropertyType.GetGenericTypeDefinition() != typeof(List<>)))
            {
                var displayAttribute = GetAttribute<DisplayAttribute>(property);

                result.Add(new PropertyInfoModel
                {
                    Type = property.DeclaringType,
                    Name = property.Name,
                    DisplayName = displayAttribute.Name,
                    Description = displayAttribute.Description,
                    GroupName = groupName,
                    ListPropertyName = listPropertyName
                });
            }
            else if (property.PropertyType.IsGenericType &&
                     property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var genGroupName = "";

                if (property.CustomAttributes.Any(c => c.AttributeType == typeof(DisplayAttribute)))
                {
                    var displayAttribute = GetAttribute<DisplayAttribute>(property);
                    if (displayAttribute != null)
                        genGroupName = displayAttribute.Name;
                }

                var genericProperties = property.PropertyType.GetGenericArguments()[0].GetProperties();

                foreach (var genProperty in genericProperties)
                    result.AddRange(GetPropertiesListRecursive(genProperty, genGroupName, property.Name));
            }

            return result;
        }

        public static T GetAttribute<T>(PropertyInfo property) where T : Attribute
        {
            var attrs = property.GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                var a = attr as T;
                if (a != null)
                    return a;
            }
            return null;
        }

        public static IEnumerable<T> GetAttributes<T>(PropertyInfo property) where T : Attribute
        {
            var attrs = property.GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                var a = attr as T;
                if (a != null)
                    yield return a;
            }
        }

        public static string GetResourceStringFromAssembly(string resourceName)
        {
            var assembly = Assembly.GetCallingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
            }

            return string.Empty;
        }

        public static byte[] GetResourceBytesFromAssembly(string resourceName)
        {
            var assembly = Assembly.GetCallingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    var resBytes = new byte[stream.Length];
                    stream.Read(resBytes, 0, resBytes.Length);
                    return resBytes;
                }
            }

            return null;
        }
    }

    [Serializable]
    public class PropertyInfoModel
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }

        public string ListPropertyName { get; set; }
        public bool Checked { get; set; }
    }
}