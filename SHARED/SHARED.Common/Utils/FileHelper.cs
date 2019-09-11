using System;
using System.IO;

namespace SHARED.Common.Utils
{
    public class FileHelper
    {
        public void RemoveAttribute(string path, FileAttributes attr)
        {
            var attributes = File.GetAttributes(path);
            if ((attributes & attr) == attr)
            {
                // remove attr.
                attributes = RemoveFileAttribute(attributes, attr);
                File.SetAttributes(path, attributes);
            }
        }

        private FileAttributes RemoveFileAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        public static bool WriteTo(string relativeAppPath, string fileName, byte[] bytes)
        {
            var serverpath = AppDomain.CurrentDomain.BaseDirectory + relativeAppPath;

            if (!Directory.Exists(serverpath)) Directory.CreateDirectory(serverpath);

            var filename = Path.GetFileName(fileName);

            try
            {
                File.WriteAllBytes(Path.Combine(serverpath, filename), bytes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}