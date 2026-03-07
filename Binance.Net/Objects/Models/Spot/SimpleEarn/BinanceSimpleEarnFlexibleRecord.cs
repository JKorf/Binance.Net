using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Flexible product subscription record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnFlexibleRecord
    {
        /// <summary>
        /// ["<c>amount</c>"] Subscribed quantity.
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
        /// ["<c>purchaseId</c>"] Purchase id
        /// </summary>
        [JsonPropertyName("purchaseId")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// ["<c>productId</c>"] Product id
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Subscription type
        /// </summary>
        [JsonPropertyName("type")]
        public SubscriptionType Type { get; set; }
        /// <summary>
        /// ["<c>sourceAccount</c>"] Source account
        /// </summary>
        [JsonPropertyName("sourceAccount")]
        public AccountSource SourceAccount { get; set; }
        /// <summary>
        /// ["<c>amtFromSpot</c>"] Quantity from spot
        /// </summary>
        [JsonPropertyName("amtFromSpot")]
        public decimal SpotQuantity { get; set; }
        /// <summary>
        /// ["<c>amtFromFunding</c>"] Quantity from funding
        /// </summary>
        [JsonPropertyName("amtFromFunding")]
        public decimal FundingQuantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public Enums.SubscriptionStatus Status { get; set; }
    }
}

