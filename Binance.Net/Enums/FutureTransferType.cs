namespace Binance.Net.Enums
{
    /// <summary>
    /// Futures account transfer type
    /// </summary>
    public enum FuturesTransferType
    {
        /// <summary>
        /// From spot to USDT-M futures account
        /// </summary>
        FromSpotToUsdtFutures,
        /// <summary>
        /// From USDT-M futures to spot account
        /// </summary>
        FromUsdtFuturesToSpot,
        /// <summary>
        /// From spot to COIN-M futures account
        /// </summary>
        FromSpotToCoinFutures,
        /// <summary>
        /// From COIN-M futures to spot account
        /// </summary>
        FromCoinFuturesToSpot
    }
}
