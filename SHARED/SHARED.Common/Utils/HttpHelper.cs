using System;
using System.Net;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace SHARED.Common.Utils
{
    public class HttpHelper
    {
        public static string LoadHtml(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        public static byte[] LoadData(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }

        public static Task<string> LoadHtmlAsync(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadStringTaskAsync(url);
            }
        }

        public static Task<byte[]> LoadDataAsync(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadDataTaskAsync(url);
            }
        }

        
    }
}