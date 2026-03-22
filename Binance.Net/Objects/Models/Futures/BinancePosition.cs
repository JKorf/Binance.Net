using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Base position info
    /// </summary>
    [SerializationModel]
    public record BinancePositionBase
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>entryPrice</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }

        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// ["<c>unrealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedPnl { get; set; }
        [JsonInclude, JsonPropertyName("unRealizedProfit")]
        internal decimal UnRealizedPnl { set => UnrealizedPnl = value; }

        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
    }

    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record BinancePositionInfoBase : BinancePositionBase
    {
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintMargin { get; set; }

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
        /// ["<c>isolated</c>"] Isolated
        /// </summary>
        [JsonPropertyName("isolated")]
        public bool Isolated { get; set; }

        /// <summary>
        /// ["<c>positionAmt</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>breakEvenPrice</c>"] Break even price
        /// </summary>
        [JsonPropertyName("breakEvenPrice")]
        public decimal BreakEvenPrice { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// Usdt position info
    /// </summary>
    [SerializationModel]
    public record BinancePositionInfoUsdt : BinancePositionInfoBase
    {
        /// <summary>
        /// ["<c>maxNotional</c>"] Max notional
        /// </summary>
        [JsonPropertyName("maxNotional")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Notional
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal Notional { get; set; }
        /// <summary>
        /// ["<c>isolatedWallet</c>"] Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }
    }

    /// <summary>
    /// Coin position info
    /// </summary>
    [SerializationModel]
    public record BinancePositionInfoCoin : BinancePositionInfoBase
    {
        /// <summary>
        /// ["<c>notionalValue</c>"] Notional value
        /// </summary>
        [JsonPropertyName("notionalValue")]
        public decimal NotionalValue { get; set; }
        /// <summary>
        /// ["<c>maxQty</c>"] Max quantity
        /// </summary>
        [JsonPropertyName("maxQty")]
        public decimal MaxQuantity { get; set; }
    }

    /// <summary>
    /// Base position details
    /// </summary>
    [SerializationModel]
    public record BinancePositionDetailsBase : BinancePositionBase
    {
        /// <summary>
        /// ["<c>marginType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// ["<c>isAutoAddMargin</c>"] Whether auto add margin is enabled.
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool IsAutoAddMargin { get; set; }

        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }

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
        /// ["<c>positionAmt</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>breakEvenPrice</c>"] Break even price
        /// </summary>
        [JsonPropertyName("breakEvenPrice")]
        public decimal BreakEvenPrice { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Usdt position details
    /// </summary>
    [SerializationModel]
    public record BinancePositionDetailsUsdt : BinancePositionDetailsBase
    {
        /// <summary>
        /// ["<c>maxNotionalValue</c>"] Max notional
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Notional value
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal Notional { get; set; }
        /// <summary>
        /// ["<c>isolatedWallet</c>"] Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }
        /// <summary>
        /// ["<c>isolated</c>"] Isolated position
        /// </summary>
        [JsonPropertyName("isolated")]
        public bool Isolated { get; set; }
        /// <summary>
        /// ["<c>adlQuantile</c>"] ADL quantile
        /// </summary>
        [JsonPropertyName("adlQuantile")]
        public decimal AdlQuantile { get; set; }
    }

    /// <summary>
    /// Coin position info
    /// </summary>
    [SerializationModel]
    public record BinancePositionDetailsCoin : BinancePositionDetailsBase
    {
        /// <summary>
        /// ["<c>notionalValue</c>"] Notional value
        /// </summary>
        [JsonPropertyName("notionalValue")]
        public decimal NotionalValue { get; set; }
        /// <summary>
        /// ["<c>maxQty</c>"] Max quantity
        /// </summary>
        [JsonPropertyName("maxQty")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// ["<c>isolatedWallet</c>"] Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }
    }
}

