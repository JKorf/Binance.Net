namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn product info
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnFlexibleProduct
    {
        /// <summary>
        /// ["<c>asset</c>"] Product asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>latestAnnualPercentageRate</c>"] Latest annual percentage rate
        /// </summary>
        [JsonPropertyName("latestAnnualPercentageRate")]
        public decimal LatestAnnualPercentageRate { get; set; }
        /// <summary>
        /// ["<c>tierAnnualPercentageRate</c>"] Tier annual percentage rate
        /// </summary>
        [JsonPropertyName("tierAnnualPercentageRate")]
        public Dictionary<string, decimal> TierAnnualPercentageRate { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// ["<c>airDropPercentageRate</c>"] Air drop percentage rate
        /// </summary>
        [JsonPropertyName("airDropPercentageRate")]
        public decimal AirDropPercentageRate { get; set; }
        /// <summary>
        /// ["<c>canPurchase</c>"] Can purchase product
        /// </summary>
        [JsonPropertyName("canPurchase")]
        public bool CanPurchase { get; set; }
        /// <summary>
        /// ["<c>canRedeem</c>"] Can redeem product
        /// </summary>
        [JsonPropertyName("canRedeem")]
        public bool CanRedeem { get; set; }
        /// <summary>
        /// ["<c>isSoldOut</c>"] Product is sold out
        /// </summary>
        [JsonPropertyName("isSoldOut")]
        public bool IsSoldOut { get; set; }
        /// <summary>
        /// ["<c>hot</c>"] Is hot
        /// </summary>
        [JsonPropertyName("hot")]
        public bool Hot { get; set; }
        /// <summary>
        /// ["<c>minPurchaseAmount</c>"] Min purchase quantity
        /// </summary>
        [JsonPropertyName("minPurchaseAmount")]
        public decimal MinPurchaseQuantity { get; set; }
        /// <summary>
        /// ["<c>productId</c>"] Product identifier.
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>subscriptionStartTime</c>"] Subscription start time
        /// </summary>
        [JsonPropertyName("subscriptionStartTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? SubscriptionStartTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}

