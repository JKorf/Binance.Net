namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Flexible product position info
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnFlexiblePosition
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
        /// ["<c>canRedeem</c>"] Can redeem product
        /// </summary>
        [JsonPropertyName("canRedeem")]
        public bool CanRedeem { get; set; }
        /// <summary>
        /// ["<c>productId</c>"] Product identifier.
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>totalAmount</c>"] Total quantity of position
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// ["<c>yesterdayAirdropPercentageRate</c>"] Air drop percentage rate of yesterday
        /// </summary>
        [JsonPropertyName("yesterdayAirdropPercentageRate")]
        public decimal YesterdayAirdropPercentageRate { get; set; }
        /// <summary>
        /// ["<c>airDropAsset</c>"] Air drop asset
        /// </summary>
        [JsonPropertyName("airDropAsset")]
        public string AirDropAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralAmount</c>"] Collateral quantity
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// ["<c>yesterdayRealTimeRewards</c>"] Realtime rewards of yesterday
        /// </summary>
        [JsonPropertyName("yesterdayRealTimeRewards")]
        public decimal YesterdayRealTimeRewards { get; set; }
        /// <summary>
        /// ["<c>cumulativeBonusRewards</c>"] Cumulative bonus rewards
        /// </summary>
        [JsonPropertyName("cumulativeBonusRewards")]
        public decimal CumulativeBonusRewards { get; set; }
        /// <summary>
        /// ["<c>cumulativeRealTimeRewards</c>"] Cumulative realtime rewards
        /// </summary>
        [JsonPropertyName("cumulativeRealTimeRewards")]
        public decimal CumulativeRealTimeRewards { get; set; }
        /// <summary>
        /// ["<c>cumulativeTotalRewards</c>"] Cumulative total rewards
        /// </summary>
        [JsonPropertyName("cumulativeTotalRewards")]
        public decimal CumulativeTotalRewards { get; set; }
        /// <summary>
        /// ["<c>autoSubscribe</c>"] Is auto subscribe enabled
        /// </summary>
        [JsonPropertyName("autoSubscribe")]
        public bool AutoSubscribe { get; set; }
    }
}

