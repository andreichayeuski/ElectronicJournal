using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SHARED.Common.Extensions;

namespace SHARED.Common.Utils
{
    public static class KeyHelper
    {

        /// <summary>
        /// Helps to create specified method cache key.
        /// 
        /// Cached method result will have key, consisting of two parts
        ///        CacheNamespace  : Created from method owning type name and method name.
        ///        ArgsKey         : Concatenation of args converted to string.
        /// </summary>
        /// <remarks>
        /// Be carefull with complex types, which do not override ToString()
        /// Because, wrong cache key will be generated.
        /// </remarks>
        public static string CreateCacheKey<T>(string methodName, params object[] args)
        {
            if (methodName == null)
            {
                throw new ArgumentNullException("methodName");
            }

            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            var innerTypeName = typeof(T).Name;
            var cacheNamespace = innerTypeName + "::" + methodName;
            var argsKey = args.Select(CreateObjectCacheKey)
                              .WhereNot(String.IsNullOrEmpty)
                              .Join("&")
                              .Replace(' ', '_');

            var key = cacheNamespace + "__" + argsKey;

            return key;
        }

        private static string CreateObjectCacheKey(object obj)
        {
            if (obj == null)
            {
                return String.Empty;
            }

            var en = obj as IEnumerable;
            if (en != null)
            {
                return CreateEnumerableObjectCacheKey(en);
            }

            var result = CreateSimpleObjectCacheKey(obj);
            return result;
        }

        private static string CreateEnumerableObjectCacheKey(IEnumerable items)
        {
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append(",")
                  .Append(CreateSimpleObjectCacheKey(item));
            }

            var result = string.Format("[{0}]",(sb.ToString()));
            return result;
        }

        private static string CreateSimpleObjectCacheKey(object obj)
        {
            var convertible = obj as IConvertible;
            if (convertible != null)
            {
                try
                {
                    return convertible.ToString(null);
                }
                catch (InvalidCastException)
                {
                    // By guidelines for usage of IConvertible interface
                    // it can throw InvalidCastException if conversion 
                    // to specified type is not supported
                }
            }

            var formattable = obj as IFormattable;
            if (formattable != null)
            {
                return formattable.ToString(null, null);
            }

            var objType = obj.GetType();
            var isSimpleType = TypeDescriptor.GetConverter(objType).CanConvertFrom(typeof (string));
            if (isSimpleType)
            {
                return obj.ToString();
            }

            return obj.ToString();
        }

    }
}
