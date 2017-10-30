using Binance.Net.Interfaces;

namespace Binance.Net.Objects
{
    public class BinanceStream
    {
        public IWebsocket Socket { get; set; }
        public int StreamId { get; set; }
        public bool UserStream { get; set; }
    }
}
