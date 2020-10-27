using Binance.Net.Interfaces.SocketSubClient;
using Binance.Net.SocketSubClients;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Interface for subscribing to streams
    /// </summary>
    public interface IBinanceSocketClient: ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        IBinanceSocketClientSpot Spot { get; set; }

        /// <summary>
        /// USDT-M futures streams
        /// </summary>
        IBinanceSocketClientFuturesUsdt FuturesUsdt { get; set; }
        /// <summary>
        /// COIN-M futures stream
        /// </summary>
        IBinanceSocketClientFuturesCoin FuturesCoin { get; set; }

        /// <summary>
        /// Leveraged token streams
        /// </summary>
        IBinanceSocketClientBlvt Blvt { get; set; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);
    }
}