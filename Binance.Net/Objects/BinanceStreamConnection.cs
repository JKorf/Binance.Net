using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Objects
{
    internal class BinanceStream
    {
        internal bool TryReconnect { get; set; } = true;
        public IWebsocket Socket { get; set; }
        public BinanceStreamSubscription StreamResult { get; set; }

        public void Close()
        {
            TryReconnect = false;
            Socket.Close();
        }
    }
}
