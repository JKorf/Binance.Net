using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Subscribe result
    /// </summary>
    public record BinanceBlvtSubscribeResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public BlvtStatus Status { get; set; }
        /// <summary>
        /// Name of the token
        /// </summary>
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Subscribed token quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Subscription cost in usdt
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
