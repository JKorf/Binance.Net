﻿using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Margin account snapshot
    /// </summary>
    public class BinanceMarginAccountSnapshot
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
        public BinanceMarginAccountSnapshotData Data { get; set; }
    }

    /// <summary>
    /// Margin snapshot data
    /// </summary>
    public class BinanceMarginAccountSnapshotData
    {
        /// <summary>
        /// The margin level
        /// </summary>
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Total BTC asset
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total BTC liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net BTC asset
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }

        /// <summary>
        /// Assets
        /// </summary>
        public List<BinanceMarginBalance> UserAssets { get; set; } = new List<BinanceMarginBalance>();
    }
}
