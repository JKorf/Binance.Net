using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Binance REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IBinanceRestClient Binance(this ICryptoRestClient baseClient) => baseClient.TryGet<IBinanceRestClient>(() => new BinanceRestClient());

        /// <summary>
        /// Get the Binance Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IBinanceSocketClient Binance(this ICryptoSocketClient baseClient) => baseClient.TryGet<IBinanceSocketClient>(() => new BinanceSocketClient());
    }
}
