namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account position risk
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountFuturesPositionRisk
    {
        /// <summary>
        /// ["<c>entryPrice</c>"] The entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>maxNotional</c>"] Max notional
        /// </summary>
        [JsonPropertyName("maxNotional")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// ["<c>liquidationPrice</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>positionAmount</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("positionAmount")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
    }
}

