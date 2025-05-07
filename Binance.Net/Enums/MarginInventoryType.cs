using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of margin inventory.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginInventoryType>))]
    public enum MarginInventoryType
    {
        /// <summary>
        /// Represents a regular margin account.
        /// </summary>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// Represents an isolated margin account.
        /// </summary>
        [Map("ISOLATED")]
        Isolated
    }
}
