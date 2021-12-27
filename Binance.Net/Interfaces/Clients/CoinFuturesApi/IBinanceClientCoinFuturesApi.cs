using CryptoExchange.Net.ExchangeInterfaces;
using System;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance Coin futures API endpoints
    /// </summary>
    public interface IBinanceClientCoinFuturesApi : IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientCoinFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientCoinFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientCoinFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the IExchangeClient for this client
        /// </summary>
        /// <returns></returns>
        IExchangeClient AsExchangeClient();
    }
}
