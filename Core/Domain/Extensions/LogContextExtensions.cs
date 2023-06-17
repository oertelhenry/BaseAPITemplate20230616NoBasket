using System;

namespace Serilog.Context
{
    public static class LogContextExtensions
    {
        public static IDisposable AddKey(object value)
        {
            return LogContext.PushProperty("Key", value);
        }

        public static IDisposable AddReponseBody(object value)
        {
            return LogContext.PushProperty("ResponseBody", value);
        }
    }
}