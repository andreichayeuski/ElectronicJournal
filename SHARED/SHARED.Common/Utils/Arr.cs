using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Utils
{
    public static class Arr
    {
        public static T[] Empty<T>()
        {
            return new T[0];
        }

        public static T[] Single<T>(T item)
        {
            return new[] { item };
        }
    }
}
