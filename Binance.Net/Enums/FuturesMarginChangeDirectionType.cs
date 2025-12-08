using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The direction to change futures margin
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesMarginChangeDirectionType>))]
    public enum FuturesMarginChangeDirectionType
    {
        /// <summary>
        /// Add margin
        /// </summary>
        [Map("1")]
        Add,
        /// <summary>
        /// Reduce Margin
        /// </summary>
        [Map("2")]
        Reduce
    }
}
