using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Snapshot data of a spot account
    /// </summary>
    public class BinanceSpotAccountSnapshot
    {
        /// <summary>
        /// Timestamp of the data
        /// </summary>
        [JsonConverter(typeof(TimestampConverter)), JsonProperty("updateTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Account type the data is for
        /// </summary>
        [JsonConverter(typeof(AccountTypeConverter))]
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
    public class BinanceSpotAccountSnapshotData
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
