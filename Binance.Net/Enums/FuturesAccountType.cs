using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Futures account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesAccountType>))]
    public enum FuturesAccountType
    {
        /// <summary>
        /// ["<c>1</c>"] USDT Margined Futures
        /// </summary>
        [Map("1")]
        UsdtMarginedFutures,
        /// <summary>
        /// ["<c>2</c>"] COIN Margined Futures
        /// </summary>
        [Map("2")]
        CoinMarginedFutures
    }
}

