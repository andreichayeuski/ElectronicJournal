using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TValue> Flatten<TNode, TValue>(
               this IEnumerable<TNode> @this,
               Func<TNode, TValue> getValue,
               Func<TNode, IEnumerable<TNode>> getChildren)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (getChildren == null)
            {
                throw new ArgumentNullException("getChildren");
            }

            var values = new List<TValue>();
            FlattenInner(@this, values, getValue, getChildren);

            return values;
        }

        private static void FlattenInner<TNode, TValue>(
            IEnumerable<TNode> source,
            IList<TValue> values,
            Func<TNode, TValue> getValue,
            Func<TNode, IEnumerable<TNode>> getChildren)
        {
            foreach (var node in source)
            {
                values.Add(getValue(node));
                var children = getChildren(node);
                FlattenInner(children, values, getValue, getChildren);
            }
        }

        public static List<T> AddRangeFluent<T>(this List<T> @this, IEnumerable<T> items)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }

            @this.AddRange(items);

            return @this;
        }

        public static IEnumerable<TResult> SelectDistinct<T, TResult>(
            this IEnumerable<T> @this,
            Func<T, TResult> selector)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            return @this.Select(selector).Distinct();
        }

        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> @this, Func<T, IEnumerable<T>> selector)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }

            var result = @this.SelectMany(selector);

            if (!result.Any())
            {
                return result;
            }

            return result.Concat(result.SelectManyRecursive(selector));
        }

        public static bool IsEmpty(this IEnumerable @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }

            var list = @this as IList;
            if (list != null)
            {
                return list.Count == 0;
            }

            var collection = @this as ICollection;
            if (collection != null)
            {
                return collection.Count == 0;
            }

            var enumerator = @this.GetEnumerator();
            var result = enumerator.MoveNext();

            return result;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }

            var result = !@this.Any();
            return result;
        }

        public static IEnumerable<T> Except<T>(
            this IEnumerable<T> @this,
            params T[] items)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            var result = @this.Except(items.ToArray().AsEnumerable());
            return result;
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> @this, params T[] items)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            var result = @this.Union(items.ToArray().AsEnumerable());
            return result;
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var result = new HashSet<T>(@this);
            return result;
        }

        public static IEnumerable<T> WhereNot<T>(
            this IEnumerable<T> @this,
            Func<T, bool> predicate)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = @this.Where(item => !predicate(item));
            return result;
        }

        public static bool None<T>(this IEnumerable<T> @this, Func<T, bool> predicate)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var result = !@this.Any(predicate);
            return result;
        }

        /// <summary>
        /// Returns a sequence of each element in the input sequence and its predecessor, 
        /// with the exception of the first element which is only returned as the predecessor of the second element.
        /// </summary>
        /// <typeparam name="T">Type of enumerable entry.</typeparam>
        /// <param name="this">Extension method target.</param>
        /// <returns>Sequence of each element in the input sequence and its predecessor.</returns>
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

        public static Tuple<T[], T[]> Partition<T>(
            this IEnumerable<T> @this,
            Func<T, bool> predicate)
        {
            var fst = new List<T>();
            var snd = new List<T>();

            foreach (var entry in @this)
            {
                if (predicate(entry))
                {
                    fst.Add(entry);
                }
                else
                {
                    snd.Add(entry);
                }
            }

            var result = Tuple.Create(fst.ToArray(), snd.ToArray());
            return result;
        }

        /// <summary>
        /// Splits sequence in parts of specified length.
        /// </summary>
        /// <typeparam name="T">Type of sequence entry.</typeparam>
        /// <param name="this">Extension method target.</param>
        /// <param name="partLength">Lenght of parts to split sequence to.</param>
        /// <returns>Sequence of parts with specified lenght created from target sequence.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(
            this IEnumerable<T> @this,
            int partLength)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (partLength < 0)
            {
                throw new ArgumentException();
            }

            var part = new List<T>(partLength);

            foreach (var item in @this)
            {
                if (part.Count == partLength)
                {
                    yield return part.ToArray();
                    part.Clear();
                }

                part.Add(item);
            }

            if (part.Count > 0)
            {
                yield return part;
            }
        }


    }
}
