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
        /// ["<c>a</c>"] The update data
        /// </summary>
        [JsonPropertyName("a")]
        public BinanceFuturesStreamAccountUpdateData UpdateData { get; set; } = new BinanceFuturesStreamAccountUpdateData();
        /// <summary>
        /// ["<c>T</c>"] Transaction time
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
        /// ["<c>m</c>"] Account update reason type
        /// </summary>
        [JsonPropertyName("m")]
        public AccountUpdateReason Reason { get; set; }

        /// <summary>
        /// ["<c>B</c>"] Balances
        /// </summary>
        [JsonPropertyName("B")]
        public BinanceFuturesStreamBalance[] Balances { get; set; } = Array.Empty<BinanceFuturesStreamBalance>();

        /// <summary>
        /// ["<c>P</c>"] Positions
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
        /// ["<c>a</c>"] The asset this balance is for
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>wb</c>"] The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("wb")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// ["<c>cw</c>"] The quantity that is locked in a trade
        /// </summary>
        [JsonPropertyName("cw")]
        public decimal CrossWalletBalance { get; set; }
        /// <summary>
        /// ["<c>bc</c>"] The balance change except PnL and commission
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
        /// ["<c>s</c>"] The symbol this balance is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pa</c>"] The quantity of the position
        /// </summary>
        [JsonPropertyName("pa")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>ep</c>"] The entry price
        /// </summary>
        [JsonPropertyName("ep")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>bep</c>"] The break even price
        /// </summary>
        [JsonPropertyName("bep")]
        public decimal BreakEvenPrice { get; set; }
        /// <summary>
        /// ["<c>cr</c>"] The accumulated realized PnL
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>up</c>"] The Unrealized PnL
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnrealizedPnl { get; set; }

        /// <summary>
        /// ["<c>mt</c>"] The margin type
        /// </summary>
        [JsonPropertyName("mt")]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// ["<c>iw</c>"] The isolated wallet (if isolated position)
        /// </summary>
        [JsonPropertyName("iw")]
        public decimal IsolatedMargin { get; set; }

        /// <summary>
        /// ["<c>ps</c>"] Position side.
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }
    }
}

