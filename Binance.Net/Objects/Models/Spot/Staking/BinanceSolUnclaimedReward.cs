namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Unclaimed rewards info
    /// </summary>
    [SerializationModel]
    public record BinanceSolUnclaimedReward
    {
        /// <summary>
        /// ["<c>amount</c>"] Unclaimed reward amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>rewardsAsset</c>"] Reward asset
        /// </summary>
        [JsonPropertyName("rewardsAsset")]
        public string RewardsAsset { get; set; } = string.Empty;
    }
}

