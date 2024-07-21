namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn product info
    /// </summary>
    public record BinanceSimpleEarnFlexibleProduct
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Latest annual percentage rate
        /// </summary>
        [JsonPropertyName("latestAnnualPercentageRate")]
        public decimal LatestAnnualPercentageRate { get; set; }
        /// <summary>
        /// Tier annual percentage rate
        /// </summary>
        [JsonPropertyName("tierAnnualPercentageRate")]
        public Dictionary<string, decimal> TierAnnualPercentageRate { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Air drop percentage rate
        /// </summary>
        [JsonPropertyName("airDropPercentageRate")]
        public decimal AirDropPercentageRate { get; set; }
        /// <summary>
        /// Can purchase product
        /// </summary>
        [JsonPropertyName("canPurchase")]
        public bool CanPurchase { get; set; }
        /// <summary>
        /// Can redeem product
        /// </summary>
        [JsonPropertyName("canRedeem")]
        public bool CanRedeem { get; set; }
        /// <summary>
        /// Product is sold out
        /// </summary>
        [JsonPropertyName("isSoldOut")]
        public bool IsSoldOut { get; set; }
        /// <summary>
        /// Is hot
        /// </summary>
        [JsonPropertyName("hot")]
        public bool Hot { get; set; }
        /// <summary>
        /// Min purchase quantity
        /// </summary>
        [JsonPropertyName("minPurchaseAmount")]
        public decimal MinPurchaseQuantity { get; set; }
        /// <summary>
        /// product id
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Subscription start time
        /// </summary>
        [JsonPropertyName("subscriptionStartTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? SubscriptionStartTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
