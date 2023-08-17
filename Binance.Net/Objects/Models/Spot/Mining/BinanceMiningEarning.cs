﻿using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Earning info
    /// </summary>
    public class BinanceMiningEarnings
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
        /// Profit items
        /// </summary>
        public IEnumerable<BinanceMiningAccountEarning> AccountProfits { get; set; } = Array.Empty<BinanceMiningAccountEarning>();
    }

    /// <summary>
    /// Earning info
    /// </summary>
    public class BinanceMiningAccountEarning
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Coin
        /// </summary>
        [JsonProperty("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Earning type
        /// </summary>
        [JsonConverter(typeof(BinanceEarningTypeConverter))]
        [JsonProperty("type")]
        public BinanceEarningType Type { get; set; }
        /// <summary>
        /// Sub account id
        /// </summary>
        [JsonProperty("puid")]
        public long? SubAccountId { get; set; }
        /// <summary>
        /// Mining account
        /// </summary>
        [JsonProperty("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
