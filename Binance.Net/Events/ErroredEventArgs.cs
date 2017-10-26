using System;

namespace Binance.Net.Events
{
    public class ErroredEventArgs: EventArgs
    {
        public Exception Exception { get; }
        public string Message { get; }
        
        public ErroredEventArgs(Exception exception, string message)
        {
            Exception = exception;
            Message = message;
        }
    }
}
