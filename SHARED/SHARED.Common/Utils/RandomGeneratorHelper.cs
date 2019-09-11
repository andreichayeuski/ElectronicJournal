using System;

namespace SHARED.Common.Utils
{
    public static class RandomGeneratorHelper
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        private static String _charArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
        //private static String _lowerCharArray = "abcdefghijklmnopqrstuvwxyz_";
        private static String _upperCharArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static String _digitCharArray = "0123456789";
        private static Int32 _charCount = 8;

        /// <summary>
        /// Default array: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_"
        /// </summary>
        public static String CharArray
        {
            get { return _charArray; }
            set { _charArray = value; }
        }

        /// <summary>
        /// Default char count: 8
        /// </summary>
        public static Int32 CharCount
        {
            get { return _charCount; }
            set { _charCount = value; }
        }

        public static String GenerateLogin()
        {
            return Generate();
        }

        public static String GeneratePassword()
        {
            var result = "";
            // Генерирует хотя бы одну букву в верхнем регистре и хотя бы одну цифру
            for (var i = 0; i < _charCount; i++)
            {
                int index;
                // Предпоследний символ - буква в верхнем регистре
                if (i == _charCount - 2)
                {
                    index = Random.Next(0, _upperCharArray.Length);
                    result += _upperCharArray[index];
                }
                // Последний символ - цифра
                else if (i == _charCount - 1)
                {
                    index = Random.Next(0, _digitCharArray.Length);
                    result += _digitCharArray[index];
                }
                else
                {
                    index = Random.Next(0, _charArray.Length);
                    result += _charArray[index];
                }
            }
            return result;
        }

        private static String Generate()
        {
            var result = "";
            for (var i = 0; i < _charCount; i++)
            {
                var index = Random.Next(0, _charArray.Length);
                try
                {
                    result += _charArray[index];
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        public static String Generate(int charCount = -1)
        {
            if (charCount == -1)
                charCount = _charCount;

            var result = "";
            for (var i = 0; i < charCount; i++)
            {
                var index = Random.Next(0, _charArray.Length);
                try
                {
                    result += _charArray[index];
                }
                catch (Exception)
                {
                }
            }
            return result;
        }
    }
}
