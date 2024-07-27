namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Flexible product position info
    /// </summary>
    public record BinanceSimpleEarnFlexiblePosition
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
        /// Can redeem product
        /// </summary>
        [JsonPropertyName("canRedeem")]
        public bool CanRedeem { get; set; }
        /// <summary>
        /// product id
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Total quantity of position
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Air drop percentage rate of yesterday
        /// </summary>
        [JsonPropertyName("yesterdayAirdropPercentageRate")]
        public decimal YesterdayAirdropPercentageRate { get; set; }
        /// <summary>
        /// Air drop asset
        /// </summary>
        [JsonPropertyName("airDropAsset")]
        public string AirDropAsset { get; set; } = string.Empty;
        /// <summary>
        /// Collateral quantity
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// Realtime rewards of yesterday
        /// </summary>
        [JsonPropertyName("yesterdayRealTimeRewards")]
        public decimal YesterdayRealTimeRewards { get; set; }
        /// <summary>
        /// Cumulative bonus rewards
        /// </summary>
        [JsonPropertyName("cumulativeBonusRewards")]
        public decimal CumulativeBonusRewards { get; set; }
        /// <summary>
        /// Cumulative realtime rewards
        /// </summary>
        [JsonPropertyName("cumulativeRealTimeRewards")]
        public decimal CumulativeRealTimeRewards { get; set; }
        /// <summary>
        /// Cumulative total rewards
        /// </summary>
        [JsonPropertyName("cumulativeTotalRewards")]
        public decimal CumulativeTotalRewards { get; set; }
        /// <summary>
        /// Is auto subscribe enabled
        /// </summary>
        [JsonPropertyName("autoSubscribe")]
        public bool AutoSubscribe { get; set; }
    }
}
