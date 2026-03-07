using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    // NOTE this is a bit of a weird place for this, however it is a request on the normal client since it uses
    // the /sapi/ route instead of /fapi/. For lack of a better place keep it here

    /// <summary>
    /// Snapshot data of a futures account
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountSnapshot
    {
        /// <summary>
        /// ["<c>updateTime</c>"] Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("updateTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Account type the data is for
        /// </summary>
        [JsonPropertyName("type")]
        public AccountType Type { get; set; }

        /// <summary>
        /// ["<c>data</c>"] Snapshot data
        /// </summary>
        [JsonPropertyName("data")]
        public BinanceFuturesAccountSnapshotData Data { get; set; } = default!;
    }

    /// <summary>
    /// Data of the snapshot
    /// </summary>
    public record BinanceFuturesAccountSnapshotData
    {
        /// <summary>
        /// ["<c>assets</c>"] List of assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceFuturesAsset[] Assets { get; set; } = Array.Empty<BinanceFuturesAsset>();
        /// <summary>
        /// ["<c>position</c>"] List of positions
        /// </summary>
        [JsonPropertyName("position")]
        public BinanceFuturesSnapshotPosition[] Position { get; set; } = Array.Empty<BinanceFuturesSnapshotPosition>();
    }

    /// <summary>
    /// Asset
    /// </summary>
    public record BinanceFuturesAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset name.
        /// </summary>
        [JsonPropertyName("asset")]
        public string? Asset { get; set; }
        /// <summary>
        /// ["<c>marginBalance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>walletBalance</c>"] Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal? WalletBalance { get; set; }
    }

    /// <summary>
    /// Position
    /// </summary>
    public record BinanceFuturesSnapshotPosition
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] The mark price.
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// ["<c>positionAmt</c>"] Position amount
        /// </summary>
        [JsonPropertyName("positionAmt")]
        public decimal? PositionAmt { get; set; }
        /// <summary>
        /// ["<c>unRealizedProfit</c>"] Unrealized profit
        /// </summary>
        [JsonPropertyName("unRealizedProfit")]
        public decimal? UnrealizedProfit { get; set; }
    }
}

