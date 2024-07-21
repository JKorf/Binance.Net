namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking product info
    /// </summary>
    public record BinanceStakingProduct
    {
        /// <summary>
        /// Project id
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Product details
        /// </summary>
        [JsonPropertyName("detail")]
        public BinanceStakingProductDetails Details { get; set; } = null!;
        /// <summary>
        /// Product quota
        /// </summary>
        public BinanceStakingQuota Quota { get; set; } = null!;
    }

    /// <summary>
    /// Staking product details
    /// </summary>
    public record BinanceStakingProductDetails
    {
        /// <summary>
        /// Lock up asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Reward asset
        /// </summary>
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Duration in days
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// Renewable
        /// </summary>
        public bool Renewable { get; set; }
        /// <summary>
        /// Apy
        /// </summary>
        public decimal Apy { get; set; }
    }

    /// <summary>
    /// Staking product quota
    /// </summary>
    public record BinanceStakingQuota
    {
        /// <summary>
        /// Total Personal quota
        /// </summary>
        [JsonPropertyName("totalPersonalQuota")]
        public decimal Quota { get; set; }
        /// <summary>
        /// Minimum amount per order
        /// </summary>
        public decimal Minimum { get; set; }
    }
}
