using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Margined futures type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IfNewUserMarginedFuturesType>))]
    public enum IfNewUserMarginedFuturesType
    {
        /// <summary>
        /// ["<c>1</c>"] Processing
        /// </summary>
        [Map("1")]
        UsdtMarginedFutures = 1,
        /// <summary>
        /// ["<c>2</c>"] Canceled
        /// </summary>
        [Map("2")]
        CoinMarginedFutures = 2,
    }
}

