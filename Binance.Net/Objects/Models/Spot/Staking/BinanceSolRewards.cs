namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL rewards
    /// </summary>
    [SerializationModel]
    public record BinanceSolRewards : BinanceQueryRecords<BinanceSolReward>
    {
        /// <summary>
        /// ["<c>estRewardsInSOL</c>"] Estimated rewards in SOL
        /// </summary>
        [JsonPropertyName("estRewardsInSOL")]
        public decimal EstimatedRewardsInSol { get; set; }
    }

    /// <summary>
    /// SOL reward
    /// </summary>
    [SerializationModel]
    public record BinanceSolReward
    {
        /// <summary>
        /// ["<c>time</c>"] The reward timestamp.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>amountInSOL</c>"] Amount in SOL
        /// </summary>
        [JsonPropertyName("amountInSOL")]
        public decimal AmountInSol { get; set; }
        /// <summary>
        /// ["<c>holding</c>"] Current holding.
        /// </summary>
        [JsonPropertyName("holding")]
        public decimal Holding { get; set; }
        /// <summary>
        /// ["<c>holdingInSOL</c>"] Holding in SOL
        /// </summary>
        [JsonPropertyName("holdingInSOL")]
        public decimal HoldingInSol { get; set; }
        /// <summary>
        /// ["<c>annualPercentageRate</c>"] Annual percentage rate.
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
    }
}

