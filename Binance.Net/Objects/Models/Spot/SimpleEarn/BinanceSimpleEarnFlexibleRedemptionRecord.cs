using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn flexible product redemption record
    /// </summary>
    public record BinanceSimpleEarnFlexibleRedemptionRecord
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Project id
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Redeem id
        /// </summary>
        [JsonPropertyName("redeemId")]
        public long RedeemId { get; set; }
        /// <summary>
        /// Destination account
        /// </summary>
        [JsonPropertyName("destAccount"), JsonConverter(typeof(EnumConverter))]
        public AccountSource DestinationAccount { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
