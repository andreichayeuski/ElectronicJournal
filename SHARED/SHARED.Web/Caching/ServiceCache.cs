using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace SHARED.Web.Caching
{
    public interface IServiceCache
    {
        void Put(string key, object value);

        void Put(string key, object value, TimeSpan expires);

        void Remove(string key);

        void RemoveStartWith(string key);

        void Clear();

        bool TryGet<T>(string key, out T result);

        T GetOrAdd<T>(string key, Func<T> factory);

        T GetOrAdd<T>(string key, Func<T> factory, TimeSpan expires);

        T AddOrReplace<T>(string key, T value);

        T AddOrReplace<T>(string key, Func<T> factory);

        T AddOrReplace<T>(string key, Func<T> factory, TimeSpan expires);
    }

    public class ServiceCache : IServiceCache
    {
        private static IMemoryCache _cache;
        private CancellationTokenSource _resetCacheToken;
        //private static readonly HashSet<string> CacheKeys = new HashSet<string>();

        private void ValidateObject(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
        }

        private MemoryCacheEntryOptions GetOptions(TimeSpan? expires)
        {
            var options = new MemoryCacheEntryOptions();
            options.SetPriority(CacheItemPriority.Normal);
            options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

            if (expires != null)
                options.SetAbsoluteExpiration(expires.Value);

            return options;
        }

        public ServiceCache()
        {
            lock (CacheManager.Locker)
            {
                if (_cache == null)
                    _cache = new MemoryCache(new MemoryCacheOptions());
                _resetCacheToken = new CancellationTokenSource();
            }
        }

        public ServiceCache(IMemoryCache cache)
        {
            lock (CacheManager.Locker)
            {
                _cache = cache ?? throw new ArgumentNullException(nameof(cache));
                _resetCacheToken = new CancellationTokenSource();
            }
        }

        public void Put(string key, object value)
        {
            ValidateObject(value);
            _cache.Set(key, value, GetOptions(null));
            //CacheKeys.Add(key);
        }

        public void Put(string key, object value, TimeSpan expires)
        {
            ValidateObject(value);
            _cache.Set(key, value, GetOptions(expires));
            //CacheKeys.Add(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            //CacheKeys.Remove(key);
        }

        public void RemoveStartWith(string key)
        {
            var keysToRemove = GetKeys().Where(x => x.StartsWith(key)).ToArray();

            foreach (var keyToRemove in keysToRemove)
            {
                Remove(keyToRemove);
            }

        }

        private List<string> GetKeys()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(_cache) as ICollection;
            var items = new List<string>();
            if (collection != null)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    items.Add(val.ToString());
                }

            return items;
        }

        public void Clear()
        {
            lock (CacheManager.Locker)
            {
                if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
                {
                    _resetCacheToken.Cancel();
                    _resetCacheToken.Dispose();
                }

                _resetCacheToken = new CancellationTokenSource();
                //CacheKeys.Clear();
            }
        }

        public bool TryGet<T>(string key, out T result)
        {
            if (_cache.TryGetValue(key, out result))
            {
                return true;
            }

            result = default(T);
            return false;
        }

        public T GetOrAdd<T>(string key, Func<T> factory)
        {
            lock (CacheManager.Locker)
            {
                if (!TryGet(key, out T result))
                {
                    result = factory();
                    Put(key, result);
                }

                return result;
            }
        }

        public T GetOrAdd<T>(string key, Func<T> factory, TimeSpan expires)
        {
            lock (CacheManager.Locker)
            {
                if (!TryGet(key, out T result))
                {
                    result = factory();
                    Put(key, result, expires);
                }

                return result;
            }
        }

        public T AddOrReplace<T>(string key, T value)
        {
            lock (CacheManager.Locker)
            {
                if (GetKeys().Contains(key))
                    Remove(key);

                Put(key, value);
                return value;
            }
        }

        public T AddOrReplace<T>(string key, Func<T> factory)
        {
            lock (CacheManager.Locker)
            {
                T result = factory();
                if (GetKeys().Contains(key))
                    Remove(key);

                Put(key, result);
                return result;
            }
        }

        public T AddOrReplace<T>(string key, Func<T> factory, TimeSpan expires)
        {
            lock (CacheManager.Locker)
            {
                T result = factory();
                if (GetKeys().Contains(key))
                    Remove(key);

                Put(key, result, expires);
                return result;
            }
        }
    }
}