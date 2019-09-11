using System;

namespace SHARED.Common.Utils
{
    public static class CodeGenerator
    {
        public static string GenerateFormCode()
        {
            return DateTime.Now.ToString("1yyyyMMddmmHHssfff");
        }
        public static string GenerateTemplateCode()
        {
            return DateTime.Now.ToString("2yyyyMMddmmHHssfff");
        }

        public static string GenerateReportCode()
        {
            return DateTime.Now.ToString("3yyyyMMddmmHHssfff");
        }
    }
}
