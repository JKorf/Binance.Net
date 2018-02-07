using System;

namespace Binance.Net.Objects
{
    public class BinanceStreamSubscription
    {
        /// <summary>
        /// Event when the socket is closed
        /// </summary>
        public event Action Closed;

        /// <summary>
        /// Event when an error occures on the socket
        /// </summary>
        public event Action<Exception> Error;

        internal int StreamId { get; set; }

        internal void InvokeClosed()
        {
            Closed?.Invoke();
        }

        internal void InvokeError(Exception ex)
        {
            Error?.Invoke(ex);
        }
    }
}
