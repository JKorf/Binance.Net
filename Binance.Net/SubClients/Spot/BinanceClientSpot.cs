using Binance.Net.Interfaces.SubClients;
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
        /// Spot system endpoints
        /// </summary>
        public IBinanceClientSpotSystem System { get; }
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
        public IBinanceClientUserStream UserStream { get; }

        internal BinanceClientSpot(Log log, BinanceClient baseClient)
        {
            System = new BinanceClientSpotSystem(log, baseClient);
            Market = new BinanceClientSpotMarket(baseClient);
            Order = new BinanceClientSpotOrder(log, baseClient);
            UserStream = new BinanceClientSpotUserStream(baseClient);
        }
    }
}
