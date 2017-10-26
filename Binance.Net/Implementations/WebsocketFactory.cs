using Binance.Net.Interfaces;
using WebSocketSharp;

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
