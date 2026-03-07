namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Account configuration
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountConfiguration
    {
        /// <summary>
        /// ["<c>feeTier</c>"] Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }
        /// <summary>
        /// ["<c>canTrade</c>"] Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>canDeposit</c>"] Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>dualSidePosition</c>"] Dual side position
        /// </summary>
        [JsonPropertyName("dualSidePosition")]
        public bool DualSidePosition { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>multiAssetsMargin</c>"] Multi assets margin
        /// </summary>
        [JsonPropertyName("multiAssetsMargin")]
        public bool MultiAssetsMargin { get; set; }
        /// <summary>
        /// ["<c>tradeGroupId</c>"] Trade group id
        /// </summary>
        [JsonPropertyName("tradeGroupId")]
        public long TradeGroupId { get; set; }
    }


}

