using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHARED.Common.Utils
{
    public class PersonalNumberChecker
    {
        private const int Length = 14;
        private const int StartIndex = 10;
        private const char StartLetter = 'A';

        private static readonly int[] Weights = new int[13] { 7, 3, 1, 7, 3, 1, 7, 3, 1, 7, 3, 1, 7 };

        /// <summary>
        /// Проверка соответствия контрольной суммы
        /// </summary>
        public static bool CheckSum(string personalNumber)
        {
            //если строка неправильного размера либо пустая
            if (String.IsNullOrEmpty(personalNumber)) return false;
            if (personalNumber.Length != Length) return false;
            personalNumber = CyrToLat(personalNumber); // Замена Кирилицы на Латиницу
            int digit;
            var array = new int[Length];

            for (int i = 0; i < array.Length; i++)
                if (Int32.TryParse(personalNumber[i].ToString(), out digit)) array[i] = digit;
                else array[i] = ParseLetter(personalNumber[i]);

            for (int i = 0; i < Length - 1; i++) array[i] *= Weights[i];

            return array.Take(13).Sum() % 10 == array.Last();
        }

        private static int ParseLetter(char letter)
        {
            return StartIndex + Math.Abs(StartLetter - letter);
        }

        private static readonly Dictionary<string, string> words = new Dictionary<string, string>()
                                                        {
                                                            {"А", "A"},
                                                            {"В", "B"},
                                                            {"Е", "E"},
                                                            {"З", "3"},
                                                            {"К", "K"},
                                                            {"М", "M"},
                                                            {"Н", "H"},
                                                            {"О", "O"},
                                                            {"Р", "P"},
                                                            {"С", "C"},
                                                            {"Т", "T"},
                                                            {"У", "Y"},
                                                            {"Х", "X"},
                                                        };

        public static String CyrToLat(String personNumber)
        {
            if (String.IsNullOrEmpty(personNumber)) return null;
            return words.Aggregate(personNumber, (current, pair) => current.Replace(pair.Key, pair.Value));
        }
    }
}
