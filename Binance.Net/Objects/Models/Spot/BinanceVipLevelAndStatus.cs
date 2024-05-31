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
        [JsonProperty("vipLevel")]
        public int VipLevel { get; set; }
        /// <summary>
        /// Is margin enabled
        /// </summary>
        [JsonProperty("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }
        /// <summary>
        /// Is futures enabled
        /// </summary>
        [JsonProperty("isFutureEnabled")]
        public bool IsFuturesEnabled { get; set; }
    }
}
