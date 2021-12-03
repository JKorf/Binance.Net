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
    }
}
