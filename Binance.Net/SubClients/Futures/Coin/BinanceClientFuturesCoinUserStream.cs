namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// COIN-M futures account endpoints
    /// </summary>
    public class BinanceClientFuturesCoinUserStream : BinanceClientFuturesUserStream
    {
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        internal BinanceClientFuturesCoinUserStream(BinanceClient baseClient, BinanceClientFutures futuresClient): base(baseClient, futuresClient) { }
    }
}
