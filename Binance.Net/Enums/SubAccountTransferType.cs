namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    public enum SubAccountFuturesTransferType
    {
        /// <summary>
        /// From sub account spot to sub account usdt-m futures
        /// </summary>
        FromSpotToUsdtFutures,
        /// <summary>
        /// From sub account usdt-m futures to sub account spot
        /// </summary>
        FromUsdtFuturesToSpot,
        /// <summary>
        /// From sub account spot to sub account coin-m futures
        /// </summary>
        FromSpotToCoinFutures,
        /// <summary>
        /// From sub account coin-m futures to sub account spot
        /// </summary>
        FromCoinFuturesToSpot
    }
}
