using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Client for accessing the Binance Spot API. 
    /// </summary>
    public interface IBinanceClientSpotMarket
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientSpotMarketAccount Account { get; }

        /// <summary>
        /// Endpoints related to BLvt
        /// </summary>
        public IBinanceClientSpotMarketLeveragedTokens LeveragedTokens { get; }
        /// <summary>
        /// Endpoints related to BSwap
        /// </summary>
        public IBinanceClientSpotMarketLiquidSwap LiquidSwap { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        public IBinanceClientSpotMarketExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientSpotMarketTrading Trading { get; }
    }
}
