using Binance.Net.Converters;
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
        /// Processing
        /// </summary>
        [Map("1")]
        UsdtMarginedFutures = 1,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("2")]
        CoinMarginedFutures = 2,
    }
}
