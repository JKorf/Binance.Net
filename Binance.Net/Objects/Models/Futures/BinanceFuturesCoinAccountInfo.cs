namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinAccountInfo
    {
        /// <summary>
        /// Whether deposits are allowed.
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Whether trading is allowed.
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Whether withdrawals are allowed.
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }
        /// <summary>
        /// Update tier
        /// </summary>
        [JsonPropertyName("updateTier")]
        public int UpdateTier { get; set; }

        /// <summary>
        /// Account assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceFuturesAccountAsset[] Assets { get; set; } = Array.Empty<BinanceFuturesAccountAsset>();
        /// <summary>
        /// Account positions
        /// </summary>
        [JsonPropertyName("positions")]
        public BinancePositionInfoCoin[] Positions { get; set; } = Array.Empty<BinancePositionInfoCoin>();
        /// <summary>
        /// The account update time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
