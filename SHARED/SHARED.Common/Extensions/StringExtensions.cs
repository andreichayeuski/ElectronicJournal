using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SHARED.Common.Utils;

namespace SHARED.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToStringWithValue(this object o)
        {
            if (o == null)
                return String.Empty;

            return o.ToString();
        }

        public static string ToShortDateStringWithValue(this DateTime? o)
        {
            if (o == null)
                return String.Empty;

            return o.Value.ToShortDateString();
        }

        public static int ToInt(this string o)
        {
            int i = 0;

            int.TryParse(o, out i);

            return i;
        }

        public static float ToFloat(this string o)
        {
            float i = 0;

            float.TryParse(o, out i);

            return i;
        }

        public static bool ToBool(this string o)
        {
            if (o.Equals("1"))
                return true;
            return false;
        }

        public static DateTime? ToDateTime(this string o)
        {
            try
            {
                return DateTime.Parse(o);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string EncodeTo64(this string toEncode)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(this string encodedData)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
            string returnValue = Encoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static string RemoveTrailingSlash(this string source)
        {
            return source.TrimEnd('/');
        } 
        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private const string CommentSymbol = "--";

        public static string RemoveComments(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return @this;

            var cleanedListing = @this.Split("\n").Select(t =>
            {
                if (t.StartsWith(CommentSymbol))
                {
                    return string.Empty;
                }

                return t.Before(CommentSymbol);
            }).Join("\n");

            return cleanedListing;
        }

        public static string ToRusPhrase(this int o)
        {
            try
            {
                return RusNumberPhrase.RusSpelledOut(o, true);
            }
            catch (Exception)
            {
                return o.ToString();

            }
        }
        public static string ToStringMonth(this object o, bool year)
        {
            if (o == null)
                return String.Empty;
            if (!year)
                return o.ToString();
            switch (o.ToString())
            {
                case "1":
                    return "Январь";
                case "2":
                    return "Февраль";
                case "3":
                    return "Март";
                case "4":
                    return "Апрель";
                case "5":
                    return "Май";
                case "6":
                    return "Июнь";
                case "7":
                    return "Июль";
                case "8":
                    return "Август";
                case "9":
                    return "Сентябрь";
                case "10":
                    return "Октябрь";
                case "11":
                    return "Ноябрь";
                case "12":
                    return "Декабрь";
                default:
                    return o.ToString();
            }

        }

        static public string ToUppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        public static string ToLowercaseFirst(this string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
            {
                return str;
            }

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        static public string Replace(this string s, string textToReplace, string replacedText, int replaceFromPosition)
        {
            if (s == null)
            {
                throw new ArgumentException("input string");
            }

            if (s.Length < replaceFromPosition)
            {
                throw new ArgumentException("replace position could be more then length of string");
            }

            if (textToReplace == null)
            {
                throw new ArgumentNullException("textToReplace");
            }

            if (replacedText == null)
            {
                throw new ArgumentNullException("replacedText");
            }

            return s.Substring(0, replaceFromPosition) + s.Substring(replaceFromPosition).Replace(textToReplace, replacedText);
        }

        static public string Before(this string s, params string[] beforeValues)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (beforeValues == null || !beforeValues.Any())
                throw new ArgumentNullException("beforeValues");

            if (beforeValues.All(t=> !s.Contains(t)))
            {
                return s;
            }

            var startIndex = beforeValues.Select(t => s.IndexOf(t, StringComparison.InvariantCulture)).Where(t=> t >= 0).OrderBy(t => t).First();

            return s.Substring(0, startIndex);
        }

        static public string After(this string s, params string[] afterValues)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (afterValues == null || !afterValues.Any())
                throw new ArgumentNullException("afterValues");

            if (afterValues.All(t => !s.Contains(t)))
            {
                return s;
            }

            var startObject = afterValues.Select(t => new { Index = s.IndexOf(t, StringComparison.InvariantCulture), SubLength = t.Length }).Where(t=>t.Index >=0).OrderBy(t => t.Index).First();

            return s.Substring(startObject.Index + startObject.SubLength);
        }


        /// <summary>
        /// Concatenates the members of string collection, using the specified separator between each member.
        /// </summary>
        /// <param name="this">Extension method target. Collection of strings to concatenate.</param>
        /// <param name="separator">String to separate collection members with.</param>
        /// <returns>Result of string collection concatenation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Join(this IEnumerable<string> @this, string separator = "")
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var result = String.Join(separator, @this);
            return result;
        }


        /// <summary>
        /// Concatenates the members of string collection, using the specified separator between each member.
        /// </summary>
        /// <param name="this">Extension method target. Collection of strings to concatenate.</param>
        /// <param name="separator">Character to separate collection members with.</param>
        /// <returns>Result of string collection concatenation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Join(this IEnumerable<string> @this, char separator)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var result = String.Join(separator.ToString(CultureInfo.CurrentCulture), @this);
            return result;
        }

        public static List<string> ParseDomainString( this string data, string delimiter)
        {
            if (data == null) return null;
            if (!delimiter.EndsWith("=")) delimiter = delimiter + "=";
            if (!data.Contains(delimiter)) return null;
            //base case
            var result = new List<string>();
            int start = data.IndexOf(delimiter) + delimiter.Length;
            int length = data.IndexOf(',', start) - start;
            if (length == 0) return null; //the group is empty
            if (length > 0)
            {
                result.Add(data.Substring(start, length));
                //only need to recurse when the comma was found, because there could be more groups
                var rec = ParseDomainString(data.Substring(start + length), delimiter);
                if (rec != null) result.AddRange(rec); //can't pass null into AddRange() :(
            }
            else //no comma found after current group so just use the whole remaining string
            {
                result.Add(data.Substring(start));
            }
            return result;
        }

        public static string DeclinateMonth(this int month, WordCase wordCase)
        {
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            return monthName.DeclinateMonth(wordCase);
        }


        public static string DeclinateMonth(this string monthName,WordCase wordCase)
        {
            var month = monthName.ToLower();

            var namesArray=new string[]
            {
                "январь","февраль","март","апрель","май","июнь","июль","август","сентябрь","октябрь","ноябрь","декабрь",
                "января","февраля","марта","апреля","мая","июня","июля","августа","сентября","октября","ноября","декабря",
                "январю","февралю","марту","апрелю","маю","июню","июлю","августу","сентябрю","октябрю","ноябрю","декабрю",
                "январь","февраль","март","апрель","май","июнь","июль","август","сентябрь","октябрь","ноябрь","декабрь",
                "январем","февралем","мартом","апрелем","маем","июнем","июлем","августом","сентябрем","октябрем","ноябрем","декабрем",
                "январе","феврале","марте","апреле","мае","июне","июле","августе","сентябре","октябре","ноябре","декабре",
            };

            var monthInArray = namesArray.FirstOrDefault(c => month.Contains(c));

            if (monthInArray != null)
            {
                var declinatedMonth = namesArray[Array.IndexOf(namesArray, monthInArray)+(int) wordCase*12];
                return month.Replace(monthInArray,declinatedMonth);
            }

            return monthName;
            
        }

        public static string[] Split(
            this string @this,
            string separator,
            StringSplitOptions splitOptions = StringSplitOptions.None)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }

            var result = @this.Split(new[] { separator }, splitOptions);
            return result;
        }

        public static string GetFileExtension(this string fileName)
        {
            string ext = string.Empty;
            int fileExtPos = fileName.LastIndexOf(".", StringComparison.Ordinal);
            if (fileExtPos >= 0)
                ext = fileName.Substring(fileExtPos, fileName.Length - fileExtPos);

            return ext;
        }

        public static bool Contains(this string @this, string compareStr, StringComparison comp)
        {
            return @this.IndexOf(compareStr, comp) >= 0;
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static string[] Split(this string @text, SplitCondition condition)
        {
            switch (condition)
            {
                case SplitCondition.R:
                    return @text.Split('\r');
                case SplitCondition.N:
                    return @text.Split('\n');
                case SplitCondition.Rn:
                    return @text.Split(new[] { "\r\n" }, StringSplitOptions.None);
                case SplitCondition.All:
                    return @text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            return @text.Split();
        }

        public enum WordCase
        {
            Именительный=0,
            Родительный=1, 
            Дательный=2,
            Винительный=3,
            Творительный=4,
            Предложный=5
        }

        public enum SplitCondition
        {
            R = 0,
            N = 1,
            Rn = 2,
            All = 3
        }
        public static string ConvertBoolToString(this bool value)
        {
            return value ? "Да" : "Нет";
        }
        public static string ConvertNBoolToString(this bool? value)
        {
            return value.HasValue ? value.Value ? "Да" : "Нет" : "";
        }
    }
}