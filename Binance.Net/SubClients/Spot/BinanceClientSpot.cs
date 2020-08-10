using Binance.Net.Interfaces.SubClients.Spot;
using CryptoExchange.Net.Logging;

namespace Binance.Net.SubClients.Spot
{
    /// <summary>
    /// Spot endpoints
    /// </summary>
    public class BinanceClientSpot : IBinanceClientSpot
    {
        /// <summary>
        /// Spot market endpoints
        /// </summary>
        public IBinanceClientSpotMarket Market { get; }
        /// <summary>
        /// Spot order endpoints
        /// </summary>
        public IBinanceClientSpotOrder Order { get; }
        /// <summary>
        /// Spot user stream endpoints
        /// </summary>
        public IBinanceClientSpotUserStream UserStream { get; }

        internal BinanceClientSpot(Log log, BinanceClient baseClient)
        {
            Market = new BinanceClientSpotMarket(baseClient);
            Order = new BinanceClientSpotOrder(log, baseClient);
            UserStream = new BinanceClientSpotUserStream(baseClient);
        }
    }
}
