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
        /// Low urgency
        /// </summary>
        [Map("LOW")]
        Low,
        /// <summary>
        /// Medium urgency
        /// </summary>
        [Map("MEDIUM")]
        Medium,
        /// <summary>
        /// High urgency
        /// </summary>
        [Map("HIGH")]
        High
    }
}
