namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Unclaimed rewards info
    /// </summary>
    [SerializationModel]
    public record BinanceSolUnclaimedReward
    {
        /// <summary>
        /// Unclaimed reward amount.
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
