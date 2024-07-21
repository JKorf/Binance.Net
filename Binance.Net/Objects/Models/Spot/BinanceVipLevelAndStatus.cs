namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// VIP level and futures/margin enabled status
    /// </summary>
    public record BinanceVipLevelAndStatus
    {
        /// <summary>
        /// VIP level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
        /// <summary>
        /// Is margin enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// Is futures enabled
        /// </summary>
        [JsonPropertyName("isFutureEnabled")]
        public bool IsFuturesEnabled { get; set; }
    }
}
