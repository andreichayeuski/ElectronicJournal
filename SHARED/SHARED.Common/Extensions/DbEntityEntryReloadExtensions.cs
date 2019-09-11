using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SHARED.Common.Extensions
{
    public static class DbEntityEntryReloadExtensions
    {
        private static readonly ConcurrentDictionary<Type, Func<IQueryable, object>> GetQueryValueMethodCache =
            new ConcurrentDictionary<Type, Func<IQueryable, object>>();

        //public static void ReloadCollections<TEntity>(
        //    this DbEntityEntry<TEntity> @this)
        //    where TEntity : class
        //{
        //    if (@this == null)
        //    {
        //        throw new ArgumentNullException();
        //    }

        //    var entityProperties = typeof (TEntity).GetProperties();
        //    var navigationProperties = entityProperties.Where(IsNavigationProperty).ToList();

        //    foreach (var property in navigationProperties)
        //    {
        //        var dbCollectionEntry = @this.Collection(property.Name);
        //        if (dbCollectionEntry.IsLoaded)
        //        {
        //            var dbValue = GetNavigationCollectionValue(@this, property);
        //            dbCollectionEntry.CurrentValue = dbValue;
        //        }
        //    }
        //}

        //private static object GetNavigationCollectionValue<TEntity>(
        //    DbEntityEntry<TEntity> @this,
        //    PropertyInfo property)
        //    where TEntity : class
        //{
        //    var queryEntryType = property.PropertyType.GetGenericArguments().First();
        //    var query = @this.Collection(property.Name).Query();

        //    var getQueryValue =
        //        GetQueryValueMethodCache.GetOrAdd(
        //            queryEntryType,
        //            CreateGetQueryValueMethod);

        //    return getQueryValue(query);
        //}

        private static bool IsNavigationProperty(PropertyInfo prop)
        {
            var propertyType = prop.PropertyType;
            var result =
                !propertyType.IsValueType && // Unneccessary checks optimization
                propertyType.IsGenericType &&
                !propertyType.IsArray && // Array[T] implements ICollection<T>
                (propertyType.IsClosedGeneric(typeof (ICollection<>)) ||
                 propertyType.ImplementsInterface(typeof (ICollection<>)));

            return result;
        }

        private static Func<IQueryable, object> CreateGetQueryValueMethod(Type queryEntryType)
        {
            // Func<IQueryable, object> 
            //     collection => collection.Cast<{queryEntryType}>().ToList<{queryEntryType}>();
            var castMethod = typeof (Queryable).GetMethod("Cast").MakeGenericMethod(queryEntryType);
            var toListMethod = typeof (Enumerable).GetMethod("ToList").MakeGenericMethod(queryEntryType);

            var param = Expression.Parameter(typeof (IQueryable));
            var castInvokedParam = Expression.Call(castMethod, param);
            var toListInvokedParam = Expression.Call(toListMethod, castInvokedParam);

            var lambda =
                Expression
                    .Lambda<Func<IQueryable, object>>(toListInvokedParam, param)
                    .Compile();

            return lambda;
        }
    }
}