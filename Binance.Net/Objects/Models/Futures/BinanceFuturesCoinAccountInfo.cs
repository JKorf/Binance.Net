namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinAccountInfo
    {
        /// <summary>
        /// ["<c>canDeposit</c>"] Whether deposits are allowed.
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>canTrade</c>"] Whether trading is allowed.
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Whether withdrawals are allowed.
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>feeTier</c>"] Fee tier
        /// </summary>
        [JsonPropertyName("feeTier")]
        public int FeeTier { get; set; }
        /// <summary>
        /// ["<c>updateTier</c>"] Update tier
        /// </summary>
        [JsonPropertyName("updateTier")]
        public int UpdateTier { get; set; }

        /// <summary>
        /// ["<c>assets</c>"] Account assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceFuturesAccountAsset[] Assets { get; set; } = Array.Empty<BinanceFuturesAccountAsset>();
        /// <summary>
        /// ["<c>positions</c>"] Account positions
        /// </summary>
        [JsonPropertyName("positions")]
        public BinancePositionInfoCoin[] Positions { get; set; } = Array.Empty<BinancePositionInfoCoin>();
        /// <summary>
        /// ["<c>updateTime</c>"] The account update time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}

