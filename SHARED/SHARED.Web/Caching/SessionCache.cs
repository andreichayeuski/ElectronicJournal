using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SHARED.Common.Utils;

namespace SHARED.Web.Caching
{
    public interface ISessionCache
    {
        void Put(string key, object value);

        void Remove(string key);

        void RemoveStartWith(string key);

        void Clear();

        bool TryGet<T>(string key, out T result);

        T GetOrAdd<T>(string key, Func<T> factory);

        T AddOrReplace<T>(string key, T value);

        T AddOrReplace<T>(string key, Func<T> factory);
    }

    public class SessionCache : ISessionCache
    {
        private readonly ISession _session;

        private void ValidateObject(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
        }

        public SessionCache(IHttpContextAccessor accessor)
        {
            _session = accessor.HttpContext.Session;
        }

        public void Put(string key, object value)
        {
            ValidateObject(value);
            _session.SetString(key, JsonUtils.ToJson(value));
        }

        public void Remove(string key)
        {
            _session.Remove(key);
        }

        public void RemoveStartWith(string key)
        {
            foreach (var keyToRemove in _session.Keys.Where(x => x.StartsWith(key)).ToArray())
            {
                Remove(keyToRemove);
            }
        }

        public void Clear()
        {
            _session.Clear();
        }

        public bool TryGet<T>(string key, out T result)
        {
            if (_session.TryGetValue(key, out var value))
            {
                result = JsonUtils.FromJsonBytes<T>(value);
                return true;
            }

            result = default(T);
            return false;
        }

        public T GetOrAdd<T>(string key, Func<T> factory)
        {
            if (!TryGet(key, out T result))
            {
                result = factory();
                Put(key, result);
            }

            return result;
        }

        public T AddOrReplace<T>(string key, T value)
        {
            if(_session.Keys.Contains(key))
                Remove(key);

            Put(key, value);
            return value;
        }

        public T AddOrReplace<T>(string key, Func<T> factory)
        {
            T result = factory();
            if (_session.Keys.Contains(key))
                Remove(key);

            Put(key, result);
            return result;
        }
    }
}