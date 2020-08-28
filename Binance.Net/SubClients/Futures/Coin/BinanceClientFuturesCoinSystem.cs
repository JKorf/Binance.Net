using CryptoExchange.Net.Logging;

namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// COIN-M futures system endpoints
    /// </summary>
    public class BinanceClientFuturesCoinSystem : BinanceClientFuturesSystem
    {
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        internal BinanceClientFuturesCoinSystem(Log log, BinanceClient baseClient, BinanceClientFutures futuresClient) :
            base(log, baseClient, futuresClient)
        {
        }
    }
}
