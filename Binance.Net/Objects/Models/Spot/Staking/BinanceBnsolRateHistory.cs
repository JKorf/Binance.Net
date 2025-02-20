namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rate history
    /// </summary>
    public record BinanceBnsolRateHistory
    {
        /// <summary>
        /// Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// Annual percentage rate
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// Boost rewards
        /// </summary>
        [JsonPropertyName("boostRewards")]
        public IEnumerable<BinanceBnsolReward> BoostRewards { get; set; } = [];
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Reward info
    /// </summary>
    public record BinanceBnsolReward
    {
        /// <summary>
        /// Boost APR
        /// </summary>
        [JsonPropertyName("boostAPR")]
        public decimal BoostApr { get; set; }
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonPropertyName("rewardsAsset")]
        public string RewardAsset { get; set; } = string.Empty;
    }
}
