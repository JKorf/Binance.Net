using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position information
    /// </summary>
    public record BinancePositionV3
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Position amount
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal PositionAmt { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Break even price
        /// </summary>
        [JsonPropertyName("breakEvenPrice")]
        public decimal BreakEvenPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Unrealized profit
        /// </summary>
        [JsonPropertyName("unRealizedProfit")]
        public decimal UnrealizedProfit { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidationPrice")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// Notional
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal Notional { get; set; }
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("marginAsset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Isolated wallet
        /// </summary>
        [JsonPropertyName("isolatedWallet")]
        public decimal IsolatedWallet { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
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
        /// Auto deleverage
        /// </summary>
        [JsonPropertyName("adl")]
        public decimal Adl { get; set; }
        /// <summary>
        /// Bid notional
        /// </summary>
        [JsonPropertyName("bidNotional")]
        public decimal BidNotional { get; set; }
        /// <summary>
        /// Ask notional
        /// </summary>
        [JsonPropertyName("askNotional")]
        public decimal AskNotional { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// Max notional value of the position
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public decimal? MaxNotionalValue { get; set; }
        /// <summary>
        /// Max notional value of the position
        /// </summary>
        [JsonPropertyName("marginType")]
        public FuturesMarginType? MarginType { get; set; }
        /// <summary>
        /// Is auto add margin enabled
        /// </summary>
        [JsonPropertyName("isAutoAddMargin")]
        public bool? IsAutoAddMargin { get; set; }
    }
}
