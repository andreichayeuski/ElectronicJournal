using System;
using System.Security.Cryptography;
using System.Text;

namespace SHARED.Common.Utils
{
    public static class HashHelper
    {
        /// <summary>
        /// Хэширует текст
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string Sha1Encrypt(string phrase)
        {
            var encoder = new UTF8Encoding();
            var sha1hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1hasher.ComputeHash(encoder.GetBytes(phrase));
            return ByteArrayToString(hashedDataBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputArray"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] inputArray)
        {
            var output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        public static byte[] HexToBytes(string hexString)
        {
            int numberChars = hexString.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] HexToBytes2(string hexString)
        {
            int numberChars = hexString.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 2);
            return bytes;
        }

        public static byte[] ReverseBytes(byte[] originalBytes)
        {
            var newBytes = new byte[originalBytes.Length];
            
            for (int i = originalBytes.Length-1, k = 0; i >= 0; i --, k ++)
                newBytes[k] = originalBytes[i];
            return newBytes;
        }
    }
}
