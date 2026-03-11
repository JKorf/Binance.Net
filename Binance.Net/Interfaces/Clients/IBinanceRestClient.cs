using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Binance.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Binance Rest API. 
    /// </summary>
    public interface IBinanceRestClient : IRestClient
    {
        /// <summary>
        /// General API endpoints
        /// </summary>
        /// <see cref="IBinanceRestClientGeneralApi"/>
        IBinanceRestClientGeneralApi GeneralApi { get; }
        /// <summary>
        /// Coin futures API endpoints
        /// </summary>
        /// <see cref="IBinanceRestClientCoinFuturesApi"/>
        IBinanceRestClientCoinFuturesApi CoinFuturesApi { get; }
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IBinanceRestClientSpotApi"/>
        IBinanceRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usd futures API endpoints
        /// </summary>
        /// <see cref="IBinanceRestClientUsdFuturesApi"/>
        IBinanceRestClientUsdFuturesApi UsdFuturesApi { get; }
    }
}
