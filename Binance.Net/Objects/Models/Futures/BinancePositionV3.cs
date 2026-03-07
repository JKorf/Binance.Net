using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position information
    /// </summary>
    [SerializationModel]
    public record BinancePositionV3
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>positionAmt</c>"] Position amount
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal PositionAmt { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>breakEvenPrice</c>"] Break even price
        /// </summary>
        [JsonPropertyName("breakEvenPrice")]
        public decimal BreakEvenPrice { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>unRealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unRealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
        [JsonInclude, JsonPropertyName("unrealizedProfit")]
        internal decimal UnrealizedProfitInt
        {
            get => UnrealizedProfit;
            set => UnrealizedProfit = value;
        }
        /// <summary>
        /// ["<c>liquidationPrice</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Notional
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal Notional { get; set; }
        /// <summary>
        /// ["<c>marginAsset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("marginAsset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isolatedWallet</c>"] Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>positionInitialMargin</c>"] Position initial margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }
        /// <summary>
        /// ["<c>openOrderInitialMargin</c>"] Open order initial margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }
        /// <summary>
        /// ["<c>adl</c>"] Auto deleverage
        /// </summary>
        [JsonPropertyName("adl")]
        public decimal Adl { get; set; }
        /// <summary>
        /// ["<c>bidNotional</c>"] Bid notional
        /// </summary>
        [JsonPropertyName("bidNotional")]
        public decimal BidNotional { get; set; }
        /// <summary>
        /// ["<c>askNotional</c>"] Ask notional
        /// </summary>
        [JsonPropertyName("askNotional")]
        public decimal AskNotional { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>maxNotionalValue</c>"] Max notional value of the position
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal? MaxNotionalValue { get; set; }
        /// <summary>
        /// ["<c>marginType</c>"] The margin type.
        /// </summary>
        [JsonPropertyName("marginType")]
        public FuturesMarginType? MarginType { get; set; }
        /// <summary>
        /// ["<c>isAutoAddMargin</c>"] Is auto add margin enabled
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool? IsAutoAddMargin { get; set; }
    }
}

