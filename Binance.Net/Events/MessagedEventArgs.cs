using System;

namespace Binance.Net.Events
{
    public class MessagedEventArgs: EventArgs
    {
        public string Data { get; }
        public bool IsBinary { get; }
        public bool IsPing { get; }
        public bool IsText { get; }
        public byte[] RawData { get; }

        public MessagedEventArgs(string data, bool isBinary, bool isPing, bool isText, byte[] rawData)
        {
            Data = data;
            IsBinary = isBinary;
            IsPing = isPing;
            IsText = isText;
            RawData = rawData;
        }
    }
}
