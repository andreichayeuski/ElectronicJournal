using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHARED.Common.Extensions
{
    public static class ConverterExtensions
    {
        /// <summary>
        /// Построение массива путём разбиения строки
        /// </summary>
        /// <typeparam name="T">тип элементов массива</typeparam>
        /// <param name="input">входящая строка</param>
        /// <param name="separator">симвом чем бьём</param>
        /// <returns></returns>
        public static List<T> BuildArrayFromString<T>(this string input, char separator = ';')
        {

            var arr = new List<T>();

            if (input == null)
            {
                return null;
            }

            if (input.Contains(separator))
            {
                foreach (var value in input.Split(separator))
                {
                    if (!string.IsNullOrEmpty(value))
                        arr.Add(value.To<T>());
                }
            }
            else
            {
                arr.Add(input.To<T>());
            }
            return arr;
        }
        /// <summary>
        /// Построение строки путём склеивания элементов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputArr">входящий массив</param>
        /// <param name="separator">каким символом собираем</param>
        /// <returns></returns>
        public static string BuildStringFromArray<T>(this List<T> inputArr, char separator = ';')
        {

            var res = new StringBuilder();
            foreach (var value in inputArr)
            {
                res.Append(value + (!EqualityComparer<T>.Default.Equals(inputArr.Last(), value) ? separator.ToString() : ""));
            }
            return res.ToString();

        }
    }
}
