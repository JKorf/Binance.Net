using Binance.Net.Interfaces.SocketSubClient;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Interface for subscribing to streams
    /// </summary>
    public interface IBinanceSocketClient: ISocketClient
    {
        /// <summary>
        /// Spot subscription
        /// </summary>
        IBinanceSocketClientSpot Spot { get; set; }

        /// <summary>
        /// Futures subscriptions
        /// </summary>
        IBinanceSocketClientFutures Futures { get; set; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);
    }
}