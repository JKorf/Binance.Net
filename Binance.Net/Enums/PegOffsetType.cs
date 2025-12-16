using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Peg offset type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PegOffsetType>))]
    public enum PegOffsetType
    {
        /// <summary>
        /// Price level
        /// </summary>
        [Map("PRICE_LEVEL")]
        PriceLevel
    }
}
