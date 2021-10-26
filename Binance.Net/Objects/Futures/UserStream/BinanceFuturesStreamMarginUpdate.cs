using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.UserStream
{
    /// <summary>
    /// Margin update
    /// </summary>
    public class BinanceFuturesStreamMarginUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Cross Wallet Balance. Only pushed with crossed position margin call
        /// </summary>
        [JsonProperty("cw")]
        public decimal? CrossWalletBalance { get; set; }
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<BinanceFuturesStreamMarginPosition> Positions { get; set; } = Array.Empty<BinanceFuturesStreamMarginPosition>();
    }

    /// <summary>
    /// Update data about an margin
    /// </summary>
    public class BinanceFuturesStreamMarginPosition
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Position Side
        /// </summary>
        [JsonProperty("ps"), JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonProperty("pa")]
        public decimal PositionQuantity { get; set; }

        /// <summary>
        /// Margin type
        /// </summary>
        [JsonProperty("mt"), JsonConverter(typeof(FuturesMarginTypeConverter))]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// Isolated Wallet (if isolated position)
        /// </summary>
        [JsonProperty("iw")]
        public decimal IsolatedWallet { get; set; }

        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonProperty("mp")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Unrealized PnL
        /// </summary>
        [JsonProperty("up")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// Maintenance Margin Required
        /// </summary>
        [JsonProperty("mm")]
        public decimal MaintMargin { get; set; }
    }
}
