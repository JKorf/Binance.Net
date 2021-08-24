﻿using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Spot.Mining
{
    /// <summary>
    /// Resale list
    /// </summary>
    public class BinanceHashrateResaleDetails
    {
        /// <summary>
        /// Total number of results
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Transfer details
        /// </summary>
        public IEnumerable<BinanceHashrateResaleDetailsItem> ProfitTransferDetails { get; set; } = Array.Empty<BinanceHashrateResaleDetailsItem>();
    }

    /// <summary>
    /// Resale item
    /// </summary>
    public class BinanceHashrateResaleDetailsItem
    {
        /// <summary>
        /// From user
        /// </summary>
        public string PoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// To user
        /// </summary>
        public string ToPoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// Algorithm
        /// </summary>
        public string AlgoName { get; set; } = string.Empty;
        /// <summary>
        /// Hash rate
        /// </summary>
        public decimal Hashrate { get; set; }
        /// <summary>
        /// Start day
        /// </summary>
        [JsonConverter(typeof(TimestampStringConverter))]
        public DateTime Day { get; set; }
        /// <summary>
        /// Coin name
        /// </summary>
        [JsonProperty("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Transferred income
        /// </summary>
        public decimal Amount { get; set; }
    }
}
