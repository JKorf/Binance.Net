using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Base position info
    /// </summary>
    public record BinancePositionBase
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// Unrealized profit
        /// </summary>
        [JsonPropertyName("unrealizedProfit")]
        public decimal UnrealizedPnl { get; set; }
        [JsonInclude, JsonPropertyName("unRealizedProfit")]
        internal decimal UnRealizedPnl { set => UnrealizedPnl = value; }

        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
    }

    /// <summary>
    /// Position info
    /// </summary>
    public record BinancePositionInfoBase: BinancePositionBase
    {
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintMargin { get; set; }

        /// <summary>
        /// Position initial margin
        /// </summary>
        [JsonPropertyName("positionInitialMargin")]
        public decimal PositionInitialMargin { get; set; }

        /// <summary>
        /// Open order initial margin
        /// </summary>
        [JsonPropertyName("openOrderInitialMargin")]
        public decimal OpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Isolated
        /// </summary>
        [JsonPropertyName("isolated")]
        public bool Isolated { get; set; }

        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// Usdt position info
    /// </summary>
    public record BinancePositionInfoUsdt : BinancePositionInfoBase
    {
        /// <summary>
        /// Max notional
        /// </summary>
        [JsonPropertyName("maxNotional")]
        public decimal MaxNotional { get; set; }
    }

    /// <summary>
    /// Coin position info
    /// </summary>
    public record BinancePositionInfoCoin : BinancePositionInfoBase
    {

        /// <summary>
        /// Break even price
        /// </summary>
        [JsonPropertyName("breakEvenPrice")]
        public decimal BreakEvenPrice { get; set; }
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonPropertyName("maxQty")]
        public decimal MaxQuantity { get; set; }
    }

    /// <summary>
    /// Base position details
    /// </summary>
    public record BinancePositionDetailsBase: BinancePositionBase
    {
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// Is auto add margin
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool IsAutoAddMargin { get; set; }

        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal LiquidationPrice { get; set; }

        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Break even price
        /// </summary>
        [JsonPropertyName("breakEvenPrice")]
        public decimal BreakEvenPrice { get; set; }

        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Usdt position details
    /// </summary>
    public record BinancePositionDetailsUsdt : BinancePositionDetailsBase
    {
        /// <summary>
        /// Max notional
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// Notional value
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal Notional { get; set; }
        /// <summary>
        /// Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }
    }

    /// <summary>
    /// Coin position info
    /// </summary>
    public record BinancePositionDetailsCoin : BinancePositionDetailsBase
    {
        /// <summary>
        /// Notional value
        /// </summary>
        [JsonPropertyName("notionalValue")]
        public decimal NotionalValue { get; set; }
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonPropertyName("maxQty")]
        public decimal MaxQuantity { get; set; }
    }
}
