using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Rule type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RuleType>))]
    public enum RuleType
    {
        /// <summary>
        /// ["<c>PRICE_RANGE</c>"] Price range
        /// </summary>
        [Map("PRICE_RANGE")]
        PriceRange,
    }
}
