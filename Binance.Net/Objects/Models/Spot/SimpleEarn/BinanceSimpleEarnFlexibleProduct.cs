namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn product info
    /// </summary>
    public class BinanceSimpleEarnFlexibleProduct
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Latest annual percentage rate
        /// </summary>
        [JsonProperty("latestAnnualPercentageRate")]
        public decimal LatestAnnualPercentageRate { get; set; }
        /// <summary>
        /// Tier annual percentage rate
        /// </summary>
        [JsonProperty("tierAnnualPercentageRate")]
        public Dictionary<string, decimal> TierAnnualPercentageRate { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Air drop percentage rate
        /// </summary>
        [JsonProperty("airDropPercentageRate")]
        public decimal AirDropPercentageRate { get; set; }
        /// <summary>
        /// Can purchase product
        /// </summary>
        [JsonProperty("canPurchase")]
        public bool CanPurchase { get; set; }
        /// <summary>
        /// Can redeem product
        /// </summary>
        [JsonProperty("canRedeem")]
        public bool CanRedeem { get; set; }
        /// <summary>
        /// Product is sold out
        /// </summary>
        [JsonProperty("isSoldOut")]
        public bool IsSoldOut { get; set; }
        /// <summary>
        /// Is hot
        /// </summary>
        [JsonProperty("hot")]
        public bool Hot { get; set; }
        /// <summary>
        /// Min purchase quantity
        /// </summary>
        [JsonProperty("minPurchaseAmount")]
        public decimal MinPurchaseQuantity { get; set; }
        /// <summary>
        /// product id
        /// </summary>
        [JsonProperty("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Subscription start time
        /// </summary>
        [JsonProperty("subscriptionStartTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? SubscriptionStartTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
