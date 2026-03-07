using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Snapshot data of a spot account
    /// </summary>
    [SerializationModel]
    public record BinanceSpotAccountSnapshot
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
        public BinanceSpotAccountSnapshotData Data { get; set; } = default!;
    }

    /// <summary>
    /// Data of the snapshot
    /// </summary>
    public record BinanceSpotAccountSnapshotData
    {
        /// <summary>
        /// ["<c>totalAssetOfBtc</c>"] The total value of assets in BTC.
        /// </summary>
        [JsonPropertyName("totalAssetOfBtc")]
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// ["<c>balances</c>"] List of balances
        /// </summary>
        [JsonPropertyName("balances")]
        public BinanceBalance[] Balances { get; set; } = Array.Empty<BinanceBalance>();

    }
}

