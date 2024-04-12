﻿using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Revenue list
    /// </summary>
    public class BinanceOtherRevenueList
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
        /// Revenue items
        /// </summary>
        public IEnumerable<BinanceOtherRevenueItem> OtherProfits { get; set; } = Array.Empty<BinanceOtherRevenueItem>();
    }

    /// <summary>
    /// Revenue
    /// </summary>
    public class BinanceOtherRevenueItem
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
        public BinanceEarningType Type { get; set; }
        /// <summary>
        /// Profit quantity
        /// </summary>
        [JsonProperty("profitAmount")]
        public decimal ProfitQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public MinerStatus Status { get; set; }
    }
}
