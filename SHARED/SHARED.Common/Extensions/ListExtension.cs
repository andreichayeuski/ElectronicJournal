using System;
using System.Collections.Generic;
using System.Linq;

namespace SHARED.Common.Extensions
{
    public static class ListExtension
    {
        public static IEnumerable<Tuple<T, T>> Pairwise<T>(this IEnumerable<T> @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var en = @this.GetEnumerator();

            if (en.MoveNext())
            {
                var prev = en.Current;

                while (en.MoveNext())
                {
                    var cur = en.Current;

                    yield return Tuple.Create(prev, cur);
                    prev = cur;
                }
            }
        }

        public static List<T> Replace<T>(this List<T> @this, T oldObject, T newObject)
        {
            var prevIndex = @this.IndexOf(oldObject);
            if (prevIndex < 0)
            {
                throw new ArgumentException("oldObject doesn't exist's in collection");
            }
            @this.Remove(oldObject);
            @this.Insert(prevIndex, newObject);

            return @this;
        }

        public static List<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
