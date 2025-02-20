using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Objects.Options;

namespace Binance.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Binance websocket API
    /// </summary>
    public interface IBinanceSocketClient: ISocketClient
    {
        /// <summary>
        /// Coin futures streams
        /// </summary>
        IBinanceSocketClientCoinFuturesApi CoinFuturesApi { get; }
        /// <summary>
        /// Spot streams and requests
        /// </summary>
        IBinanceSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usd futures streams
        /// </summary>
        IBinanceSocketClientUsdFuturesApi UsdFuturesApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
