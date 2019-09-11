using System;
using NLog;

namespace SHARED.Common.Logging
{
    public abstract class SimpleLogger
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger(); 

        public static void Log(string message,params object[] parameters)
        { 
            LogManager.ThrowExceptions = true;
            var logEventInfo = new LogEventInfo(LogLevel.Trace, "*", String.Format(message,parameters));

            try
            {
                Logger.Log(logEventInfo);

            }
            catch (Exception ex)
            {
                
            }   
        }
        
        public static void Error(string message, Exception exception)
        {
            var logEventInfo = new LogEventInfo(LogLevel.Error, "*", String.Format("{0} [Message: {1}, InnerException: {2}. StackTrace: {3}]", message, exception.Message, exception.InnerException, exception.StackTrace));

            try
            {
                Logger.Log(logEventInfo);
            }
            catch (Exception e)
            {
               
            }

        }
    }
}
