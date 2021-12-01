using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients
{
    public interface IBinanceClient: IRestClient
    {
        IBinanceClientGeneralApi GeneralApi { get; }
        IBinanceClientCoinFuturesApi CoinFuturesApi { get; }
        IBinanceClientSpotApi SpotApi { get; }
        IBinanceClientUsdFuturesApi UsdFuturesApi { get; }
    }
}
