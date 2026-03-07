using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin account snapshot
    /// </summary>
    [SerializationModel]
    public record BinanceMarginAccountSnapshot
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
        public BinanceMarginAccountSnapshotData Data { get; set; } = default!;
    }

    /// <summary>
    /// Margin snapshot data
    /// </summary>
    public record BinanceMarginAccountSnapshotData
    {
        /// <summary>
        /// ["<c>marginLevel</c>"] The margin level
        /// </summary>
        [JsonPropertyName("marginLevel")]
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// ["<c>totalAssetOfBtc</c>"] Total BTC asset
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalLiabilityOfBtc</c>"] Total BTC liability
        /// </summary>
        [JsonPropertyName("totalLiabilityOfBtc")]
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// ["<c>totalNetAssetOfBtc</c>"] Total net BTC asset
        /// </summary>
        [JsonPropertyName("totalNetAssetOfBtc")]
        public decimal TotalNetAssetOfBtc { get; set; }

        /// <summary>
        /// ["<c>userAssets</c>"] Assets
        /// </summary>
        [JsonPropertyName("userAssets")]
        public BinanceMarginBalance[] UserAssets { get; set; } = Array.Empty<BinanceMarginBalance>();
    }
}

