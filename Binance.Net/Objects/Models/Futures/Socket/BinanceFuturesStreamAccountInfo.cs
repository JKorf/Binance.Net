using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Account update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamAccountUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// The update data
        /// </summary>
        [JsonPropertyName("a")]
        public BinanceFuturesStreamAccountUpdateData UpdateData { get; set; } = new BinanceFuturesStreamAccountUpdateData();
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }

        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Account update data
    /// </summary>
    public record BinanceFuturesStreamAccountUpdateData
    {
        /// <summary>
        /// Account update reason type
        /// </summary>
        [JsonPropertyName("m")]
        public AccountUpdateReason Reason { get; set; }

        /// <summary>
        /// Balances
        /// </summary>
        [JsonPropertyName("B")]
        public BinanceFuturesStreamBalance[] Balances { get; set; } = Array.Empty<BinanceFuturesStreamBalance>();

        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("P")]
        public BinanceFuturesStreamPosition[] Positions { get; set; } = Array.Empty<BinanceFuturesStreamPosition>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public record BinanceFuturesStreamBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("wb")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// The quantity that is locked in a trade
        /// </summary>
        [JsonPropertyName("cw")]
        public decimal CrossWalletBalance { get; set; }
        /// <summary>
        /// The balance change except PnL and commission
        /// </summary>
        [JsonPropertyName("bc")]
        public decimal BalanceChange { get; set; }
    }

    /// <summary>
    /// Information about an asset position
    /// </summary>
    public record BinanceFuturesStreamPosition
    {
        /// <summary>
        /// The symbol this balance is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the position
        /// </summary>
        [JsonPropertyName("pa")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The entry price
        /// </summary>
        [JsonPropertyName("ep")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// The break even price
        /// </summary>
        [JsonPropertyName("bep")]
        public decimal BreakEvenPrice { get; set; }
        /// <summary>
        /// The accumulated realized PnL
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// The Unrealized PnL
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// The margin type
        /// </summary>
        [JsonPropertyName("mt")]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// The isolated wallet (if isolated position)
        /// </summary>
        [JsonPropertyName("iw")]
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// Position Side
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }
    }
}
