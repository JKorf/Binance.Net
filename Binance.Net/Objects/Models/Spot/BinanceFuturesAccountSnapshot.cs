using System;
using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    // NOTE this is a bit of a weird place for this, however it is a request on the normal client since it uses
    // the /sapi/ route instead of /fapi/. For lack of a better place keep it here

    /// <summary>
    /// Snapshot data of a futures account
    /// </summary>
    public class BinanceFuturesAccountSnapshot
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
        public BinanceFuturesAccountSnapshotData Data { get; set; } = default!;
    }

    /// <summary>
    /// Data of the snapshot
    /// </summary>
    public class BinanceFuturesAccountSnapshotData
    {
        /// <summary>
        /// List of assets
        /// </summary>
        public IEnumerable<BinanceFuturesAsset> Assets { get; set; } = Array.Empty<BinanceFuturesAsset>();
        /// <summary>
        /// List of positions
        /// </summary>
        public IEnumerable<BinanceFuturesSnapshotPosition> Position { get; set; } = Array.Empty<BinanceFuturesSnapshotPosition>();
    }

    /// <summary>
    /// Asset
    /// </summary>
    public class BinanceFuturesAsset
    {
        /// <summary>
        /// Name of the asset
        /// </summary>
        public string? Asset { get; set; }
        /// <summary>
        /// Margin balance
        /// </summary>
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Wallet balance
        /// </summary>
        public decimal? WalletBalance { get; set; }
    }

    /// <summary>
    /// Position
    /// </summary>
    public class BinanceFuturesSnapshotPosition
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string? Symbol { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// PositionAmt
        /// </summary>
        public decimal? PositionAmt { get; set; }
        /// <summary>
        /// Unrealized profit
        /// </summary>
        public decimal? UnrealizedProfit { get; set; }
    }
}
