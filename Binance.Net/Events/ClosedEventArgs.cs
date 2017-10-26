using System;

namespace Binance.Net.Events
{
    public class ClosedEventArgs: EventArgs
    {
        public ushort Code { get; }
        public string Reason { get; }
        public bool WasClean { get; }

        public ClosedEventArgs(ushort code, string reason, bool wasClean)
        {
            Code = code;
            Reason = reason;
            WasClean = wasClean;
        }
    }
}
