using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Snapshot data of a spot account
    /// </summary>
    public record BinanceSpotAccountSnapshot
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonProperty("updateTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Account type the data is for
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public AccountType Type { get; set; }
        /// <summary>
        /// Snapshot data
        /// </summary>
        [JsonProperty("data")]
        public BinanceSpotAccountSnapshotData Data { get; set; } = default!;
    }

    /// <summary>
    /// Data of the snapshot
    /// </summary>
    public record BinanceSpotAccountSnapshotData
    {
        /// <summary>
        /// The total value of assets in btc
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// List of balances
        /// </summary>
        public IEnumerable<BinanceBalance> Balances { get; set; } = Array.Empty<BinanceBalance>();

    }
}
