using Binance.Net.Interfaces;

namespace Binance.Net.Objects
{
    public class BinanceStream
    {
        public IWebsocket Socket { get; set; }
        public BinanceStreamSubscription StreamResult { get; set; }
        public bool UserStream { get; set; }
    }
}
