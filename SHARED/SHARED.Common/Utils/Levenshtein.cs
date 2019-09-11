using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SHARED.Common.Utils
{
    public class Levenshtein
    {
        private const int I = 1;
        private const int D = 1;
        private const int R = 1;
        private const int T = 1;

        /*public static int Distance(string source, string target, int limit)
        {
            if (String.IsNullOrEmpty(source) && String.IsNullOrEmpty(target)) return 0;

            if (String.IsNullOrEmpty(source)) return target.Length;
            if (String.IsNullOrEmpty(target)) return source.Length;

            if (source.Equals(target, StringComparison.InvariantCultureIgnoreCase)) return 0;

            var s = source.ToLower();
            var t = target.ToLower();

            var r = s.Length + 1;
            var c = t.Length + 1;

            var d = new int[r, c];

            d[0, 0] = 0;

            for (int i = 1; i <= s.Length; i++) d[i, 0] = i;
            for (int j = 1; j <= t.Length; j++) d[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
                for (int j = 1; j <= t.Length; j++)
                {
                    //проверка на лимит
                    //todo: предусмотреть вариант с переполнением
                    if (i == j - 1 && d[i, j - 1] > limit) return limit + 1;

                    int transposition;

                    //проверка на выход за границы массива
                    if (i == 1 || j == 1) transposition = 1000 * T;
                    else transposition = d[i - 2, j - 2] + Transposition(s[i - 1], t[j - 2], s[i - 2], t[j - 1]);

                    d[i, j] = new[] { d[i, j - 1] + I, d[i - 1, j] + D, d[i - 1, j - 1] + Replace(s[i - 1], t[j - 1]), transposition }.Min();
                }

            return d[s.Length, t.Length];
        }*/

/*        /// <summary>
        /// Вычисление расстояния Дамерау-Левенштейна с помощью алгоритма Вагнера-Фишера
        /// </summary>
        /// <param name="source">исходная строка</param>
        /// <param name="target">целевая строка</param>
        /// <returns>расстояние Дамерау-Левенштейна</returns>
        public static int Distance(string source, string target)
        {
            if (String.IsNullOrEmpty(source) && String.IsNullOrEmpty(target)) return 0;

            if (String.IsNullOrEmpty(source)) return target.Length;
            if (String.IsNullOrEmpty(target)) return source.Length;

            if (source.Equals(target, StringComparison.InvariantCultureIgnoreCase)) return 0;

            var s = source.ToLower();
            var t = target.ToLower();

            var r = s.Length + 1;
            var c = t.Length + 1;

            var d = new int[r, c];

            d[0, 0] = 0;

            for (int i = 1; i <= s.Length; i++) d[i, 0] = i;
            for (int j = 1; j <= t.Length; j++) d[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
                for (int j = 1; j <= t.Length; j++)
                {
                    int transposition;

                    //проверка на выход за границы массива
                    if (i == 1 || j == 1) transposition = 1000 * T;
                    else transposition = d[i - 2, j - 2] + Transposition(s[i - 1], t[j - 2], s[i - 2], t[j - 1]);

                    d[i, j] = new[] { d[i, j - 1] + I, d[i - 1, j] + D, d[i - 1, j - 1] + Replace(s[i - 1], t[j - 1]), transposition }.Min();
                }

            return d[s.Length, t.Length];
        }
        */
        public static int Distance(string source, string target, int limit)
        {
            try
            {
                if (String.IsNullOrEmpty(source) && String.IsNullOrEmpty(target)) return 0;

                if (String.IsNullOrEmpty(source)) return target.Length;
                if (String.IsNullOrEmpty(target)) return source.Length;

                if (source.Equals(target, StringComparison.InvariantCultureIgnoreCase)) return 0;

                var s = source.ToLower();
                var t = target.ToLower();

                var r = s.Length + 1;
                var c = t.Length + 1;

                var d = new int[r, c];

                d[0, 0] = 0;
                for (int i = 1; i <= s.Length; i++)
                    for (int j = 0; j <= t.Length; j++)
                        d[i, j] = 100;
                int U = limit;
                if (s.Length < t.Length) U = Math.Min(s.Length, U);
                else U = Math.Min(t.Length, U);

                for (int i = 1; i <= U; i++) d[i, 0] = i;
                for (int j = 1; j <= t.Length; j++) d[0, j] = j;

                for (int j = 1; j <= t.Length; j++)
                {
                    U = Math.Min(U + 1, s.Length);

                    for (int i = 1; i <= U; i++)
                    {
                        int transposition;
                        //проверка на выход за границы массива
                        if (i == 1 || j == 1) transposition = 1000*T;
                        else transposition = d[i - 2, j - 2] + Transposition(s[i - 1], t[j - 2], s[i - 2], t[j - 1]);

                        d[i, j] =
                            new[]
                            {
                                d[i, j - 1] + I, d[i - 1, j] + D, d[i - 1, j - 1] + Replace(s[i - 1], t[j - 1]),
                                transposition
                            }
                                .Min();
                    }
                    while (d[U, j] > limit && U != 0)
                    {
                        U--;
                    }
                    if (U == 0) break;
                }
                return d[s.Length, t.Length];
            }
            catch (Exception ex)
            {
                return 100;
            }
        }

        private static int Replace(char a, char b)
        {
            return !a.Equals(b) ? R : 0;
        }

        private static int Transposition(char a, char b, char c, char d)
        {
            return (a.Equals(b) && c.Equals(d)) ? T : 1000 * T;
        }
    }
}
