using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Order urgency
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderUrgency>))]
    public enum OrderUrgency
    {
        /// <summary>
        /// ["<c>LOW</c>"] Low urgency
        /// </summary>
        [Map("LOW")]
        Low,
        /// <summary>
        /// ["<c>MEDIUM</c>"] Medium urgency
        /// </summary>
        [Map("MEDIUM")]
        Medium,
        /// <summary>
        /// ["<c>HIGH</c>"] High urgency
        /// </summary>
        [Map("HIGH")]
        High
    }
}

