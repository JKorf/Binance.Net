using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Futures.UserStream
{
    /// <summary>
    /// Account update
    /// </summary>
    public class BinanceFuturesStreamAccountUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// The update data
        /// </summary>
        [JsonProperty("a")]
        public BinanceFuturesStreamAccountUpdateData UpdateData { get; set; } = new BinanceFuturesStreamAccountUpdateData();
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// Account update data
    /// </summary>
    public class BinanceFuturesStreamAccountUpdateData
    {
        /// <summary>
        /// Account update reason type
        /// </summary>
        [JsonProperty("m"), JsonConverter(typeof(AccountUpdateReasonConverter))]
        public AccountUpdateReason Reason { get; set; }

        /// <summary>
        /// Balances
        /// </summary>
        [JsonProperty("B")]
        public IEnumerable<BinanceFuturesStreamBalance> Balances { get; set; } = new List<BinanceFuturesStreamBalance>();

        /// <summary>
        /// Positions
        /// </summary>
        [JsonProperty("P")]
        public IEnumerable<BinanceFuturesStreamPosition> Positions { get; set; } = new List<BinanceFuturesStreamPosition>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceFuturesStreamBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonProperty("a")]
        public string Asset { get; set; } = "";
        /// <summary>
        /// The amount that isn't locked in a trade
        /// </summary>
        [JsonProperty("wb")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// The amount that is locked in a trade
        /// </summary>
        [JsonProperty("cw")]
        public decimal CrossWalletBalance { get; set; }
    }

    /// <summary>
    /// Information about an asset position
    /// </summary>
    public class BinanceFuturesStreamPosition
    {
        /// <summary>
        /// The symbol this balance is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The amount of the position
        /// </summary>
        [JsonProperty("pa")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The entry price
        /// </summary>
        [JsonProperty("ep")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// The accumulated realized PnL
        /// </summary>
        [JsonProperty("cr")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// The Unrealized PnL
        /// </summary>
        [JsonProperty("up")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// The margin type
        /// </summary>
        [JsonProperty("mt"), JsonConverter(typeof(FuturesMarginTypeConverter))]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// The isolated wallet (if isolated position)
        /// </summary>
        [JsonProperty("iw")]
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// Position Side
        /// </summary>
        [JsonProperty("ps"), JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }
    }
}
