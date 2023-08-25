
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Futures account type
    /// </summary>
    public enum FuturesAccountType
    {
        /// <summary>
        /// USDT Margined Futures
        /// </summary>
        [Map("1")]
        UsdtMarginedFutures,
        /// <summary>
        /// COIN Margined Futures
        /// </summary>
        [Map("2")]
        CoinMarginedFutures
    }
}
