using System.Diagnostics;

namespace Binance.Net
{
    internal static class Log
    {
        public static LogLevel Level { get; internal set; } = LogLevel.Warning;

        public static void Write(LogLevel logType, string message)
        {
            if((int)logType >= (int)Level)
                Trace.WriteLine($"{logType} | {message}");
        }
    }

    public enum LogLevel
    {
        Debug,
        Warning,
        Error
    }
}
