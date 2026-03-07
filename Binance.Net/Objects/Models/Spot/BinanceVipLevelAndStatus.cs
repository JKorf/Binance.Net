namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// VIP level and futures/margin enabled status
    /// </summary>
    [SerializationModel]
    public record BinanceVipLevelAndStatus
    {
        /// <summary>
        /// ["<c>vipLevel</c>"] VIP level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
        /// <summary>
        /// ["<c>isMarginEnabled</c>"] Is margin enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// ["<c>isFutureEnabled</c>"] Is futures enabled
        /// </summary>
        [JsonPropertyName("isFutureEnabled")]
        public bool IsFuturesEnabled { get; set; }
        /// <summary>
        /// ["<c>isOptionsEnabled</c>"] Is options enabled
        /// </summary>
        [JsonPropertyName("isOptionsEnabled")]
        public bool IsOptionsEnabled { get; set; }
        /// <summary>
        /// ["<c>isPortfolioMarginRetailEnabled</c>"] Is portfolio margin retail enabled
        /// </summary>
        [JsonPropertyName("isPortfolioMarginRetailEnabled")]
        public bool IsPortfolioMarginRetailEnabled { get; set; }
    }
}

