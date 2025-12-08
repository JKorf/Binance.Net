namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL rewards
    /// </summary>
    [SerializationModel]
    public record BinanceSolRewards : BinanceQueryRecords<BinanceSolReward>
    {
        /// <summary>
        /// Estimated rewards in SOL
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
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Amount in SOL
        /// </summary>
        [JsonPropertyName("amountInSOL")]
        public decimal AmountInSol { get; set; }
        /// <summary>
        /// Holding
        /// </summary>
        [JsonPropertyName("holding")]
        public decimal Holding { get; set; }
        /// <summary>
        /// Holding in SOL
        /// </summary>
        [JsonPropertyName("holdingInSOL")]
        public decimal HoldingInSol { get; set; }
        /// <summary>
        /// Annual percetage ratge
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
    }
}
