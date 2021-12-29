using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using System;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot API endpoints
    /// </summary>
    public interface IBinanceClientSpotApi : IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        public IBinanceClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        ISpotClient AsISpotClient();
    }
}
