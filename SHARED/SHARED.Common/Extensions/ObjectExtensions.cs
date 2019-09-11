using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using SHARED.Common.Utils;

namespace SHARED.Common.Extensions
{
    public static class ObjectExtensions
    {
        #region To<T>()
        /// <summary>
        /// Try cast <paramref name="obj"/> value to type <typeparamref name="T"/>,
        /// if can't will return default(<typeparamref name="T"/>)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T To<T>(this object obj)
        {
            return To(obj, default(T));
        }

        /// <summary>
        /// Try cast <paramref name="obj"/> value to type <typeparamref name="T"/>,
        /// if can't will return <paramref name="defaultValue"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T To<T>(this object obj, T defaultValue)
        {
            if (obj == null)
                return defaultValue;

            if (obj is T)
                return (T)obj;

            Type type = typeof(T);

            // Place convert to reference types here

            if (type == typeof(string))
            {
                return (T)(object)obj.ToString();
            }

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return To(obj, defaultValue, underlyingType);
            }

            return To(obj, defaultValue, type);
        }

        private static T To<T>(object obj, T defaultValue, Type type)
        {
            if (obj is bool && type.IsNumericType())
            {
                return (bool)obj ? (T)Convert.ChangeType(1, type) : (T)Convert.ChangeType(0, type);
            }

            if (type == typeof(int))
            {
                int intValue;
                if (int.TryParse(obj.ToString(), out intValue))
                    return (T)(object)intValue;
                return defaultValue;
            }

            if (type == typeof(decimal))
            {
                decimal decimalValue;
                if (decimal.TryParse(obj.ToString(), out decimalValue))
                    return (T)(object)decimalValue;
                return defaultValue;
            }

            if (type == typeof(double))
            {
                double doubleValue;
                if (double.TryParse(obj.ToString(), out doubleValue))
                    return (T)(object)doubleValue;
                return defaultValue;
            }

            if (type == typeof(long))
            {
                long intValue;
                if (long.TryParse(obj.ToString(), out intValue))
                    return (T)(object)intValue;
                return defaultValue;
            }

            if (type == typeof(bool))
            {
                if (obj.GetType().IsNumericType())
                    return (T)(object)(Convert.ToInt64(obj) != 0);
                bool bValue;
                if (bool.TryParse(obj.ToString(), out bValue))
                    return (T)(object)bValue;
                return defaultValue;
            }

            if (type == typeof(byte))
            {
                byte byteValue;
                if (byte.TryParse(obj.ToString(), out byteValue))
                    return (T)(object)byteValue;
                return defaultValue;
            }

            if (type == typeof(short))
            {
                short shortValue;
                if (short.TryParse(obj.ToString(), out shortValue))
                    return (T)(object)shortValue;
                return defaultValue;
            }

            if (type == typeof(DateTime))
            {
                DateTime dateValue;
                if (DateTime.TryParse(obj.ToString(), out dateValue))
                    return (T)(object)dateValue;
                return defaultValue;
            }

            if (type.IsEnum)
            {
                
                if (Enum.IsDefined(type, obj.ToString().Replace(" ", "_")))
                    return (T)Enum.Parse(type, obj.ToString().Replace(" ", "_"), true);
                return defaultValue;
            }

            throw new NotSupportedException(string.Format("Couldn't parse to Type {0}", typeof(T)));
        }

        private static bool IsNumericType(this Type type)
        {
            return type == typeof(byte) || type == typeof(short) || type == typeof(int) || type == typeof(long) ||
                   type == typeof(float) || type == typeof(double) || type == typeof(ushort) || type == typeof(uint) ||
                   type == typeof(ulong);
        }
        #endregion To<T>()

        public static string GetMd5Hash(this object instance)
        {
            return instance.GetHash<MD5CryptoServiceProvider>();
        }

        public static string GetSha1Hash(this object instance)
        {
            return instance.GetHash<SHA1CryptoServiceProvider>();
        }

        public static string GetHash<T>(this object instance) where T : HashAlgorithm, new()
        {
            T cryptoServiceProvider = new T();
            return ComputeHash(instance, cryptoServiceProvider);
        }

        public static string GetKeyedHash<T>(this object instance, byte[] key) where T : KeyedHashAlgorithm, new()
        {
            var cryptoServiceProvider = new T { Key = key };
            return ComputeHash(instance, cryptoServiceProvider);
        }
        private static string ComputeHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm, new()
        {
            var serializer = new DataContractSerializer(instance.GetType());
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(cryptoServiceProvider.Hash);
            }
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttr(this object obj, Type[] attributeTypes, Type[] excludedAttributeTypes = null)
        {
            if (obj == null)
                return Arr.Empty<PropertyInfo>();

            var props = obj.GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes(true).Any(t => attributeTypes.Any(at => at == t.GetType())));

            if (excludedAttributeTypes != null)
            {
                return props.Where(p =>
                    excludedAttributeTypes.All(e => p.GetCustomAttribute(e) == null)
                );
            }

            return props;
        }

        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }

        public static T[,] AppendColumnOnTheLeft<T>(T[,] before)
        {
            var after = new T[before.GetLength(0), before.GetLength(1) + 1];
            for (var i = 0; i < before.GetLength(0); ++i)
                for (var j = 0; j < before.GetLength(1); ++j)
                    after[i, j + 1] = before[i, j];

            return after;
        }

        public static T[,] AppendColumnOnTheRight<T>(T[,] before)
        {
            var after = new T[before.GetLength(0), before.GetLength(1) + 1];
            for (var i = 0; i < before.GetLength(0); ++i)
                for (var j = 0; j < before.GetLength(1); ++j)
                    after[i, j] = before[i, j];

            return after;
        }

        public static object Cast(this object obj, Type t)
        {
            try
            {
                var param = Expression.Parameter(obj.GetType());
                return Expression.Lambda(Expression.Convert(param, t), param)
                    .Compile().DynamicInvoke(obj);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public static ExpandoObject ToExpandoObject(this object obj)
        {
            var expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj.GetType()))
            {
                
                expando.TryAdd(property.Name, property.GetValue(obj));
            }

            return expando;
        }
    }
}
