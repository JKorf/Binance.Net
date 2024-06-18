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
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Project id
        /// </summary>
        [JsonProperty("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Redeem id
        /// </summary>
        [JsonProperty("redeemId")]
        public long RedeemId { get; set; }
        /// <summary>
        /// Destination account
        /// </summary>
        [JsonProperty("destAccount"), JsonConverter(typeof(EnumConverter))]
        public AccountSource DestinationAccount { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
