using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Base position info
    /// </summary>
    public class BinancePositionBase
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Entry price
        /// </summary>
        public decimal EntryPrice { get; set; }

        /// <summary>
        /// Leverage
        /// </summary>
        public int Leverage { get; set; }
        /// <summary>
        /// Unrealized profit
        /// </summary>
        [JsonProperty("unrealizedProfit")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// Position side
        /// </summary>
        [JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }
    }

    /// <summary>
    /// Position info
    /// </summary>
    public class BinancePositionInfoBase: BinancePositionBase
    {
        /// <summary>
        /// Initial margin
        /// </summary>
        public decimal InitialMargin { get; set; }

        /// <summary>
        /// Maint margin
        /// </summary>
        public decimal MaintMargin { get; set; }

        /// <summary>
        /// Position initial margin
        /// </summary>
        public decimal PositionInitialMargin { get; set; }
        
        /// <summary>
        /// Open order initial margin
        /// </summary>
        public decimal OpenOrderInitialMargin { get; set; }

        /// <summary>
        /// Isolated
        /// </summary>
        public bool Isolated { get; set; }

        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonProperty("positionAmt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Usdt position info
    /// </summary>
    public class BinancePositionInfoUsdt : BinancePositionInfoBase
    {
        /// <summary>
        /// Max notional
        /// </summary>
        public decimal MaxNotional { get; set; }
    }

    /// <summary>
    /// Coin position info
    /// </summary>
    public class BinancePositionInfoCoin : BinancePositionInfoBase
    {
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonProperty("maxQty")]
        public decimal MaxQuantity { get; set; }
    }

    /// <summary>
    /// Base position details
    /// </summary>
    public class BinancePositionDetailsBase: BinancePositionBase
    {
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonConverter(typeof(FuturesMarginTypeConverter))]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// Is auto add margin
        /// </summary>
        public bool IsAutoAddMargin { get; set; }

        /// <summary>
        /// Isolated margin
        /// </summary>
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// Liquidation price
        /// </summary>
        public decimal LiquidationPrice { get; set; }

        /// <summary>
        /// Mark price
        /// </summary>
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonProperty("positionAmt")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Usdt position details
    /// </summary>
    public class BinancePositionDetailsUsdt : BinancePositionDetailsBase
    {
        /// <summary>
        /// Max notional
        /// </summary>
        [JsonProperty("maxNotionalValue")]
        public decimal MaxNotional { get; set; }
    }

    /// <summary>
    /// Coin position info
    /// </summary>
    public class BinancePositionDetailsCoin : BinancePositionDetailsBase
    {
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonProperty("maxQty")]
        public decimal MaxQuantity { get; set; }
    }
}
