namespace Binance.Net.SubClients.Futures.Usdt
{
    /// <summary>
    /// USDT-M futures account user stream endpoint 
    /// </summary>
    public class BinanceClientFuturesUsdtUserStream : BinanceClientFuturesUserStream
    {
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "fapi";

        internal BinanceClientFuturesUsdtUserStream(BinanceClient baseClient, BinanceClientFutures futuresClient): base(baseClient, futuresClient) { }
    }
}
