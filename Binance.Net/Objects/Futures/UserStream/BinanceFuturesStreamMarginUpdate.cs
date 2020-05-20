using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.UserStream
{
    /// <summary>
    /// Update data about an margin
    /// </summary>
    public class BinanceFuturesStreamMarginUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Position Side
        /// </summary>
        [JsonProperty("ps"), JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Position Amount
        /// </summary>
        [JsonProperty("pa")]
        public decimal PositionAmount { get; set; }

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
        public decimal UnrealizedPNL { get; set; }

        /// <summary>
        /// Maintenance Margin Required
        /// </summary>
        [JsonProperty("mm")]
        public decimal MaintMargin { get; set; }
    }
}
