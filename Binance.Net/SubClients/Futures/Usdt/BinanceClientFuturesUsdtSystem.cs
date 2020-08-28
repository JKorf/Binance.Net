using CryptoExchange.Net.Logging;

namespace Binance.Net.SubClients.Futures.Usdt
{
    /// <summary>
    /// USDT-M futures system endpoints
    /// </summary>
    public class BinanceClientFuturesUsdtSystem : BinanceClientFuturesSystem
    {
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "fapi";

        internal BinanceClientFuturesUsdtSystem(Log log, BinanceClient baseClient, BinanceClientFutures futuresClient) :
            base(log, baseClient, futuresClient)
        {
        }
    }
}
