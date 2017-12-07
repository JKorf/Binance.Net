using Binance.Net.Interfaces;
using WebSocket4Net;

namespace Binance.Net.Implementations
{
    public class WebsocketFactory : IWebsocketFactory
    {
        public IWebsocket CreateWebsocket(string url)
        {
            return new BinanceSocket(new WebSocket(url));
        }
    }
}
