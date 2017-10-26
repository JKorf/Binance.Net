using System;
using System.IO;

namespace Binance.Net.Logging
{
    internal class Log
    {
        public TextWriter TextWriter { get; internal set; } = new TraceTextWriter();
        public LogVerbosity Level { get; internal set; } = LogVerbosity.Warning;

        public void Write(LogVerbosity logType, string message)
        {
            if((int)logType >= (int)Level)
                TextWriter.WriteLine($"{DateTime.Now:hh:mm:ss:fff} | {logType} | {message}");
        }
    }

    public enum LogVerbosity
    {
        Debug,
        Warning,
        Error
    }
}
