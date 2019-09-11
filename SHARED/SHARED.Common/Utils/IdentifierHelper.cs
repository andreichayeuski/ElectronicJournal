using System;

namespace SHARED.Common.Utils
{
    public static class IdentifierHelper
    {
        // generates unique id
        public static int GenerateIdentifier()
        {
            var guidBytes = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt32(guidBytes, 0);
        }
    }
}
