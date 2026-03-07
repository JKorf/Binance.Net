using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn flexible product redemption record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnFlexibleRedemptionRecord
    {
        /// <summary>
        /// ["<c>amount</c>"] Redeemed quantity.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] Product asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>projectId</c>"] Project id
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>redeemId</c>"] Redeem id
        /// </summary>
        [JsonPropertyName("redeemId")]
        public long RedeemId { get; set; }
        /// <summary>
        /// ["<c>destAccount</c>"] Destination account
        /// </summary>
        [JsonPropertyName("destAccount")]
        public AccountSource DestinationAccount { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}

