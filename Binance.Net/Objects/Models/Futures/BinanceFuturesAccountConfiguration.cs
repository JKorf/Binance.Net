namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Account configuration
    /// </summary>
    public record BinanceFuturesAccountConfiguration
    {
        /// <summary>
        /// Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Dual side position
        /// </summary>
        [JsonPropertyName("dualSidePosition")]
        public bool DualSidePosition { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Multi assets margin
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetsMargin { get; set; }
        /// <summary>
        /// Trade group id
        /// </summary>
        [JsonPropertyName("tradeGroupId")]
        public long TradeGroupId { get; set; }
    }


}
