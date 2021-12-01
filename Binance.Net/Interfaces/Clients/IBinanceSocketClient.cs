using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients
{
    public interface IBinanceSocketClient: ISocketClient
    {
        IBinanceSocketClientCoinFuturesStreams CoinFuturesStreams { get; }
        IBinanceSocketClientSpotStreams SpotStreams { get; }
        IBinanceSocketClientUsdFuturesStreams UsdFuturesStreams { get; }
    }
}
