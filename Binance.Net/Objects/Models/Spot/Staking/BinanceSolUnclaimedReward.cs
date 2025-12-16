namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Unclaimed rewards info
    /// </summary>
    [SerializationModel]
    public record BinanceSolUnclaimedReward
    {
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonPropertyName("rewardsAsset")]
        public string RewardsAsset { get; set; } = string.Empty;
    }
}
