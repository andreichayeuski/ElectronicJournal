using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// разбиваем нашу историю в группы по 2 штуки
        // 1,2,3,4 => {1,2}, {2,3}, {3,4}
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T, T>> GroupBy2Objects<T>(this T[] @this)
        {
            for (int i = 0; i < @this.Count() - 1; i++)
            {
                yield return Tuple.Create(@this[i], @this[i + 1]);
            }
        }
    }
}
