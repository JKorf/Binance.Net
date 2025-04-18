using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Subscribe result
    /// </summary>
    [SerializationModel]
    public record BinanceBlvtSubscribeResult
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public BlvtStatus Status { get; set; }
        /// <summary>
        /// Name of the token
        /// </summary>
        [JsonPropertyName("tokenName")]
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Subscribed token quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Subscription cost in usdt
        /// </summary>
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
