using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Locked product subscription record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedRecord
    {
        /// <summary>
        /// Subscribed quantity.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Product asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Position identifier.
        /// </summary>
        [JsonPropertyName("positionId"), JsonConverter(typeof(NumberStringConverter))]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Subscription timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Purchase identifier.
        /// </summary>
        [JsonPropertyName("purchaseId")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// Project identifier.
        /// </summary>
        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Subscription type
        /// </summary>
        [JsonPropertyName("type")]
        public SubscriptionType Type { get; set; }
        /// <summary>
        /// Source account
        /// </summary>
        [JsonPropertyName("sourceAccount")]
        public AccountSource SourceAccount { get; set; }
        /// <summary>
        /// Quantity from spot
        /// </summary>
        [JsonPropertyName("amtFromSpot")]
        public decimal SpotQuantity { get; set; }
        /// <summary>
        /// Quantity from funding
        /// </summary>
        [JsonPropertyName("amtFromFunding")]
        public decimal FundingQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public Enums.SubscriptionStatus Status { get; set; }
        /// <summary>
        /// Lock period in days.
        /// </summary>
        [JsonPropertyName("lockPeriod")]
        public int LockPeriod { get; set; }
    }
}
