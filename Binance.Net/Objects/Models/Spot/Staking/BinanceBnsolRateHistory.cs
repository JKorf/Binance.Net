namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rate history
    /// </summary>
    [SerializationModel]
    public record BinanceBnsolRateHistory
    {
        /// <summary>
        /// ["<c>exchangeRate</c>"] Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// ["<c>annualPercentageRate</c>"] Annual percentage rate
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// ["<c>boostRewards</c>"] Boost rewards
        /// </summary>
        [JsonPropertyName("boostRewards")]
        public BinanceBnsolReward[] BoostRewards { get; set; } = [];
        /// <summary>
        /// ["<c>time</c>"] The timestamp.
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
        /// ["<c>boostAPR</c>"] Boost APR
        /// </summary>
        [JsonPropertyName("boostAPR")]
        public decimal BoostApr { get; set; }
        /// <summary>
        /// ["<c>rewardsAsset</c>"] Reward asset
        /// </summary>
        [JsonPropertyName("rewardsAsset")]
        public string RewardAsset { get; set; } = string.Empty;
    }
}

